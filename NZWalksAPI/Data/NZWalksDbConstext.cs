using Microsoft.EntityFrameworkCore;
using NZWalksAPI.Models.Domain;

namespace NZWalksAPI.Data
{
    public class NZWalksDbConstext : DbContext
    {
        public NZWalksDbConstext(DbContextOptions<NZWalksDbConstext> dbConstextOption) : base(dbConstextOption)
        {

        }
      public  DbSet<Difficulty> Difficulties { get; set; }
      public  DbSet<Region> Regions { get; set; }
      public  DbSet<Walk> Walks { get; set; }
      public  DbSet<Image> Images { get; set; }


    }
}
