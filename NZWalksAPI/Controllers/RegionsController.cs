using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalksAPI.CustomeActionFilter;
using NZWalksAPI.Data;
using NZWalksAPI.Models.Domain;
using NZWalksAPI.Models.DTO;
using NZWalksAPI.Repositories;
using System.Text.Json;

namespace NZWalksAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class RegionsController : ControllerBase
    {

        private readonly NZWalksDbConstext dbConstext;
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;
        private readonly ILogger<RegionsController> logger;

        public RegionsController(NZWalksDbConstext dbConstext,
            IRegionRepository regionRepository, IMapper mapper,
            ILogger<RegionsController> logger)
        {
            this.dbConstext = dbConstext;
            this.regionRepository = regionRepository;
            this.mapper = mapper;
            this.logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {

            try
            {
                //throw new Exception("this is a Custom exception ");


                //convert region to DTO
                //var regionsDto = new List<RegionDto>();
                // foreach (var regionDomain in regionsDomain)
                // {
                //   regionsDto.Add(new RegionDto()
                //    {

                //       id = regionDomain.id,
                //       code = regionDomain.code,
                //       Name = regionDomain.Name,
                //       RegionImageUrl = regionDomain.RegionImageUrl
                //   }); 
                // }




                var regionsDomain = await regionRepository.GetAllAsync();
                logger.LogInformation($"Finished GetAllRegion Request with date :{JsonSerializer.Serialize(regionsDomain)}");
                var regionsDto = mapper.Map<List<RegionDto>>(regionsDomain);
                return Ok(regionsDto);
            }
            catch (Exception ex)
            {

                logger.LogError(ex, ex.Message);
                throw;
            }
             
        }
        //get single region by id 
        //----------------------------------------------------------------

        [HttpGet]
        [Route("{id:Guid}")]
        //[Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            // var regionDomain =await dbConstext.Regions.Find(id);
            var regionDomain = await regionRepository.GetByIdAsync(id);
            if (regionDomain == null)
            {
                return NotFound();
            }
            //var regionDto = new RegionDto
            //{

            //    id = regionDomain.id,
            //    code = regionDomain.code,
            //    Name = regionDomain.Name,
            //    RegionImageUrl = regionDomain.RegionImageUrl
            //};

            var regionDto = mapper.Map<RegionDto>(regionDomain);
            return Ok(regionDto);
        }


        // Post to create new region 
        // post : https:/localhost:prortnumber/api/regions
        [HttpPost]
        [ValidateModel]
      //  [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {

            //    var regionDomainModel = new Region
            //    {
            //        code = addRegionRequestDto.code,
            //        Name = addRegionRequestDto.Name,
            //        RegionImageUrl = addRegionRequestDto.RegionImageUrl
            //    };
            var regionDomainModel = mapper.Map<Region>(addRegionRequestDto);
            regionDomainModel = await regionRepository.CreateAsync(regionDomainModel);

            //var regionDto = new RegionDto
            //{
            //    id = regionDomainModel.id,
            //    code = regionDomainModel.code,
            //    Name = regionDomainModel.Name,
            //    RegionImageUrl = regionDomainModel.RegionImageUrl
            //};
            var regionDto = mapper.Map<Region>(regionDomainModel);
            return CreatedAtAction(nameof(GetById), new { id = regionDomainModel.id }, regionDto);


        }

        // Update a region 
        // Put : https:/localhost:prortnumber/api/regions
        [HttpPut]
        [Route("{id:Guid}")]
      //  [Authorize(Roles = "Writer")]
        [ValidateModel]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {

            //var regionDomainModel = new Region
            //{
            //    code = updateRegionRequestDto.code,
            //    Name = updateRegionRequestDto.Name,
            //    RegionImageUrl = updateRegionRequestDto.RegionImageUrl
            //};
            var regionDomainModel = mapper.Map<Region>(updateRegionRequestDto);
            regionDomainModel = await regionRepository.UpdateAsync(id, regionDomainModel);
            if (regionDomainModel == null)
            {
                return NotFound();
            }


            //var regionDto = new RegionDto
            //{
            //    id = regionDomainModel.id,
            //    Name = regionDomainModel.Name,
            //    RegionImageUrl = regionDomainModel.RegionImageUrl
            //};

            var regionDto = mapper.Map<Region>(regionDomainModel);
            return Ok(regionDto);

        }


        //Delete a region 
        // DELETE : https:/localhost:prortnumber/api/regions
        [HttpDelete]
        [Route("{id:Guid}")]
       // [Authorize(Roles ="Writer")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var regionDomainModel = await regionRepository.DeleteAsync(id);
            if (regionDomainModel == null)
            {
                return NotFound();
            }

            //Delete region 
            // map domain model to DTO
            //var regionDto = new RegionDto
            //{
            //    id = regionDomainModel.id,
            //    Name = regionDomainModel.Name,
            //    RegionImageUrl = regionDomainModel.RegionImageUrl
            //};
            var regionDto = mapper.Map<Region>(regionDomainModel);

            return Ok(regionDto);

        }
    }
}
