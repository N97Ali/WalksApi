using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalksAPI.Models.Domain;
using NZWalksAPI.Models.DTO;
using NZWalksAPI.Repositories;

namespace NZWalksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IImageRepository iImageRepository;

        public ImageController(IImageRepository iImageRepository)
        {
            this.iImageRepository = iImageRepository;
        }
        [HttpPost]
        [Route("Upload")]
        //post : /api/images/upload
        public async Task<IActionResult> Upload([FromForm]
           ImageUploadRequestDto imageUploadRequestDto)
        {
            //Call file upload method 
            ValidateFileUpload(imageUploadRequestDto);

            if (ModelState.IsValid)
            {
                //convert dto to domain model 
                var imageDomainModel = new Image
                {
                    File = imageUploadRequestDto.File,
                    FileExtention = Path.GetExtension(imageUploadRequestDto.File.FileName),
                    FileSizeInBytes = imageUploadRequestDto.File.Length,
                    FileName = imageUploadRequestDto.FileName,
                    FileDescription = imageUploadRequestDto.FileDescription,
                };
                //user repository to upload image 
                await iImageRepository.Upload(imageDomainModel);
                return Ok(imageDomainModel);
            }
            return BadRequest(ModelState);


        }
        //File upload Method
        private void ValidateFileUpload(ImageUploadRequestDto imageUploadRequestDto)
        {
            var allowedExtensions = new string[] { ".jpg", ".jpeg", ".png" }; // Make sure the extensions start with a dot (".")
            if (allowedExtensions.Contains(Path.GetExtension(imageUploadRequestDto.File.FileName).ToLower()) == false)
            {
                ModelState.AddModelError("File", "Unsupported file extension");
            }
            if (imageUploadRequestDto.File.Length > 10485760) // 10MB in bytes
            {
                ModelState.AddModelError("file", "File size exceeds 10MB, please upload a smaller file.");
            }
        }

    }
}
