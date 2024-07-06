using Microsoft.EntityFrameworkCore;
using NZWalksAPI.Data;
using NZWalksAPI.Models.Domain;

namespace NZWalksAPI.Repositories
{
    public class SQLWalkRepository : IWalkRepository
    {
        private readonly NZWalksDbConstext nZWalksDbConstext;
        public SQLWalkRepository(NZWalksDbConstext nZWalksDbConstext)
        {
            this.nZWalksDbConstext = nZWalksDbConstext;
        }
        public async Task<Walk> CreateAsync(Walk walk)
        {
            await nZWalksDbConstext.Walks.AddRangeAsync(walk);
            await nZWalksDbConstext.SaveChangesAsync();
            return walk;
        }

        public async Task<Walk?> DeleteAsync(Guid id)
        {
            var existingWalk = await nZWalksDbConstext.Walks.FirstOrDefaultAsync(x => x.id == id);
            if (existingWalk == null)
            {
                return null;
            }
            nZWalksDbConstext.Walks.Remove(existingWalk);
            await nZWalksDbConstext.SaveChangesAsync();

            return existingWalk;
        }

        public async Task<List<Walk>> GatAllAsync(string? filterOn = null, string? filterQuery = null,
            string? sortBy = null , bool isAscending = true, int pageNumber = 1, int pageSize = 1000)
        {
            //return await nZWalksDbConstext.Walks.Include("Difficulty").Include("Region").ToListAsync();
            //filter
            var  walks = nZWalksDbConstext.Walks.Include("Difficulty").Include("Region").AsQueryable();
            if(string.IsNullOrWhiteSpace(filterOn)== false && string.IsNullOrWhiteSpace(filterQuery)== false)
            {
                if(filterOn.Equals("Name",StringComparison.OrdinalIgnoreCase))
                {
                    walks = walks.Where(x => x.Name.Contains(filterQuery));
                }
                   

            }
            //sorting 
            if(string.IsNullOrWhiteSpace(sortBy)== false)
            {
                if (sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAscending ? walks.OrderBy(x => x.Name) : walks.OrderByDescending(x => x.Name);
                }
                else if (sortBy.Equals("Length", StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAscending ? walks.OrderBy(x => x.LengthInKm) : walks.OrderByDescending(x => x.LengthInKm);
                }
            }
            //pagination 
            var skipResult = (pageNumber - 1) * pageSize; 
            
            return await walks.Skip(skipResult).Take(pageSize).ToListAsync();
        }

        public async Task<Walk?> GatByIdAsync(Guid id)
        {
            return await nZWalksDbConstext.Walks
                .Include("Difficulty")
                .Include("Region")
                .FirstOrDefaultAsync(x => x.id == id);



        }

        public async Task<Walk?> UpdateAsync(Guid id, Walk walk)
        {
            var existingWalk = await nZWalksDbConstext.Walks.FirstOrDefaultAsync(x => x.id == id);
            if (existingWalk == null)
            {
                return null;
            }
            existingWalk.Name = walk.Name;
            existingWalk.Description = walk.Description;
            existingWalk.LengthInKm = walk.LengthInKm;
            existingWalk.WalkImageUrl = walk.WalkImageUrl;
            existingWalk.DifficultyId = walk.DifficultyId;
            existingWalk.RegionId = walk.RegionId;


            await nZWalksDbConstext.SaveChangesAsync();
            return existingWalk;
        }





    }
}

