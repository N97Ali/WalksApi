using Microsoft.EntityFrameworkCore;
using NZWalksAPI.Data;
using NZWalksAPI.Models.Domain;

namespace NZWalksAPI.Repositories
{
    public class SQLRegionRepository : IRegionRepository
    {
        private readonly NZWalksDbConstext dbConstext;
        public SQLRegionRepository(NZWalksDbConstext dbConstext)
        {
            this.dbConstext = dbConstext;
        }

        public async Task<Region> CreateAsync(Region region)
        {
            await dbConstext.Regions.AddAsync(region);
            await dbConstext.SaveChangesAsync();
            return region;

        }

        public async Task<Region?> DeleteAsync(Guid id)
        {
            var existingRegion = await dbConstext.Regions.FirstOrDefaultAsync(x => x.id == id);
            if (existingRegion == null)
            {
                return null;
            }
            dbConstext.Regions.Remove(existingRegion);
           await dbConstext.SaveChangesAsync();
            return existingRegion;
        }

        public async Task<List<Region>> GetAllAsync()
        {
            return await dbConstext.Regions.ToListAsync();
        }

        public async Task<Region?> GetByIdAsync(Guid id)
        {
            return await dbConstext.Regions.FirstOrDefaultAsync(x => x.id == id);
        }

        public async Task<Region?> UpdateAsync(Guid id, Region region)
        {
            var existingRegion = await dbConstext.Regions.FirstOrDefaultAsync(x => x.id == id);
            if (existingRegion == null)
            {
                return null;
            }
            existingRegion.code = region.code;
            existingRegion.Name = region.Name;
            existingRegion.RegionImageUrl = region.RegionImageUrl;
            await dbConstext.SaveChangesAsync();
            return existingRegion;
        }
    }
}
