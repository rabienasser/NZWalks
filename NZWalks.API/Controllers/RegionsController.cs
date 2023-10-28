using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTOs;
using NZWalks.API.Services;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private NZWalksDbContext dbContext;
        private IRegionService regionService;
        private readonly IMapper mapper;

        public RegionsController(NZWalksDbContext dbContext, IRegionService regionService, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.regionService = regionService;
            this.mapper = mapper;
        }

        // GET ALL REGIONS
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var regions = await regionService.GetAllAsync();

            // Map Domain Models to DTOs using Automapper
            var regionsDto = mapper.Map<List<RegionGetDTO>>(regions);

            // Return DTOs
            return Ok(regionsDto); 
        }

        // GET REGION BY ID
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetRegionById([FromRoute] Guid id)
        {
            //var region = dbContext.Regions.Find(id);
            var region = await regionService.GetRegionByIdAsync(id);

            if (region == null)
            {
                return NotFound();
            }

            // Map Domain Model to DTO using Automapper
            var regionDto = mapper.Map<RegionGetDTO>(region);

            return Ok(regionDto);
        }

        // CREATE NEW REGION
        [HttpPost]
        public async Task<IActionResult> CreateRegion([FromBody] RegionPostDTO regionPostDTO)
        {
            // Map DTO to Domain Model
            var region = mapper.Map<Region>(regionPostDTO);

            region = await regionService.CreateRegionAsync(region);

            // Map Domain Model back to DTO
            var regionGetDto = mapper.Map<RegionGetDTO>(region);

            return CreatedAtAction(nameof(GetRegionById), new {id = regionGetDto.Id}, regionGetDto); 
        }

        // UPDATE EXISTING REGION
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateRegion([FromRoute] Guid id, [FromBody] RegionPutDTO regionPutDTO)
        {

            // Map DTO to Domain Model
            var region = mapper.Map<Region>(regionPutDTO);

            region = await regionService.UpdateRegionAsync(id, region);

            if (region == null)
            {
                return NotFound();
            }

            // Convert Domain Model back to DTO
            var regionDTO = mapper.Map<RegionGetDTO>(region);

            return Ok(regionDTO);
        }

        // DELETE REGION
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteRegion([FromRoute] Guid id)
        {
            var region = await regionService.DeleteRegionAsync(id);

            if (region == null)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
