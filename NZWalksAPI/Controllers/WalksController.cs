using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalksAPI.CustomeActionFilter;
using NZWalksAPI.Models.Domain;
using NZWalksAPI.Models.DTO;
using NZWalksAPI.Repositories;

namespace NZWalksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IWalkRepository iwalkRepository;
        public WalksController(IMapper mapper, IWalkRepository iwalkRepository)
        {
            this.mapper = mapper;
            this.iwalkRepository = iwalkRepository;
        }
        //create walk
        //post method /api/walks
        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Ceate([FromBody] AddWalkRequestDto addWalkRequestDto)
        {

            var walkDomainModel = mapper.Map<Walk>(addWalkRequestDto);

            await iwalkRepository.CreateAsync(walkDomainModel);
            //map domain model to dto 
            //var adddtomodel = mapper.map<WalkDto>
            return Ok(mapper.Map<WalkDto>(walkDomainModel));
            //map dto to domain model 


        }



        //Get Walks 
        //get filter walks filteron = name & filterQurery = track & sortBy = isAscending - true & pageNumber= 1 & pageSize = 10
        [HttpGet]

        public async Task<IActionResult> GetAll([FromQuery] string? filterOn,
            [FromQuery] string? filterQuery,
            [FromQuery] string? sortBy,
            [FromQuery] bool? isAscending,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 1000)
        {
            var walkDomainModel = await iwalkRepository.GatAllAsync(filterOn,
                filterQuery, sortBy,
                isAscending ?? true, pageNumber, pageSize);
           // throw new Exception("this new exeption : ");
            //map domain model to dto 
            return Ok(mapper.Map<List<WalkDto>>(walkDomainModel));
        }

        //Get bv id 
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var walkDomainModel = await iwalkRepository.GatByIdAsync(id);
            if (walkDomainModel == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<WalkDto>(walkDomainModel));

        }

        //update by walk by id 
        //put 
        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, UpdateWalkRequestDto updateWalkRequestDto)
        {
            var walkDomainModel = mapper.Map<Walk>(updateWalkRequestDto);

            walkDomainModel = await iwalkRepository.UpdateAsync(id, walkDomainModel);
            if (walkDomainModel == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<WalkDto>(walkDomainModel));


        }

        //delete by id  
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var deletedWalkDomainModel = await iwalkRepository.DeleteAsync(id);
            if (deletedWalkDomainModel == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<WalkDto>(deletedWalkDomainModel));
        }
    }
}




