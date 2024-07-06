using NZWalksAPI.Data;
using System.IO;
using NZWalksAPI.Models.Domain;

namespace NZWalksAPI.Repositories
{
    public class LocalImageRepository : IImageRepository
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly NZWalksDbConstext _DbConstext;

        public LocalImageRepository(IWebHostEnvironment webHostEnvironment ,
            IHttpContextAccessor httpContextAccessor,NZWalksDbConstext  _DbConstext)
        {
            this.webHostEnvironment = webHostEnvironment;
            this.httpContextAccessor = httpContextAccessor;
            this._DbConstext = _DbConstext;
        }
        
        
        
        public async Task<Image> Upload(Image image)
        {
           var localFilePath = Path.Combine(webHostEnvironment.ContentRootPath, "Images", 
            $"{image.FileName}{image.FileExtention}");
            //upload images to local path 
            using var stream = new FileStream(localFilePath, FileMode.Create);
            await image.File.CopyToAsync(stream);

            var urlFilePath = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}{httpContextAccessor.HttpContext.Request.PathBase}/Images/{image.FileName}{image.FileExtention}";


            //Add image to images table 
            await _DbConstext.Images.AddAsync(image);
            
            await _DbConstext.SaveChangesAsync();

            return image;
        }


    }
}
