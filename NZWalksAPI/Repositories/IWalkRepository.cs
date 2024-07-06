﻿using NZWalksAPI.Models.Domain;

namespace NZWalksAPI.Repositories
{
    public interface IWalkRepository
    {
        Task<Walk> CreateAsync(Walk walk);
        Task<List<Walk>> GatAllAsync(string? filterOn = null, string? filterQuery = null ,
            string? sortBy = null,bool isAscending = true , int pageNumber =1  , int pageSize= 1000);
        Task<Walk?> GatByIdAsync(Guid id);
        Task<Walk?>  UpdateAsync(Guid id,Walk walk);
        Task<Walk?> DeleteAsync(Guid id); 
    }
}
