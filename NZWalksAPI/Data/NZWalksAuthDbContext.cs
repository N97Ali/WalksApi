using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace NZWalksAPI.Data
{
    public class NZWalksAuthDbContext:IdentityDbContext
    {

        public NZWalksAuthDbContext(DbContextOptions<NZWalksAuthDbContext> option ): base(option)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            var readerRoleId = "b98705f6-983f-4f47-ad23-c00dc396576c";
            var writerRoleId = "7ba7c2c1-ebc0-4ac8-b9b9-109059e6eaf8";

            var roles = new List<IdentityRole>
            {
               new IdentityRole
               {
                   Id=readerRoleId,
                   ConcurrencyStamp  = readerRoleId,
                   Name = "Reader",
                   NormalizedName=  "Reader".ToUpper()
               },
                new IdentityRole
               {
                   Id=writerRoleId,
                   ConcurrencyStamp  = writerRoleId,
                   Name = "writer",
                   NormalizedName=  "writer".ToUpper()
               }
            };
            builder.Entity<IdentityRole>().HasData(roles);

        }


    }
}
