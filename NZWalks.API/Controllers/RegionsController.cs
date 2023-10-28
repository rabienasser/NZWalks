using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTOs;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private NZWalksDbContext dbContext;

        public RegionsController(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // GET ALL REGIONS
        [HttpGet]
        public IActionResult GetAll()
        {
            // Get data from db - Domain Model
            var regions = dbContext.Regions.ToList();

            // Map Domain Models to DTOs
            var regionsDto = new List<RegionGetDTO>();
            foreach (var region in regions)
            {
                regionsDto.Add(new RegionGetDTO()
                {
                    Id = region.Id,
                    Name = region.Name,
                    Code = region.Code,
                    RegionImageUrl = region.RegionImageUrl,
                });
            }

            // Return DTOs
            return Ok(regionsDto); 
        }

        // GET REGION BY ID
        [HttpGet]
        [Route("{id}")]
        public IActionResult GetRegionById([FromRoute] Guid id)
        {
            //var region = dbContext.Regions.Find(id);
            var region = dbContext.Regions.FirstOrDefault(r => r.Id == id);

            var regionDto = new RegionGetDTO()
            {
                Id = region.Id,
                Name = region.Name,
                Code = region.Code,
                RegionImageUrl = region.RegionImageUrl,
            };

            if(region == null)
            {
                return NotFound();
            }

            return Ok(regionDto);
        }

        // CREATE NEW REGION
        [HttpPost]
        public IActionResult CreateRegion([FromBody] RegionPostDTO regionDTO)
        {
            // Map DTO to Domain Model
            var region = new Region()
            {
                Code = regionDTO.Code,
                RegionImageUrl = regionDTO.RegionImageUrl,
                Name = regionDTO.Name
            };

            // Use Domain Modal to create Region
            dbContext.Regions.Add(region);
            dbContext.SaveChanges();

            // Map Domain Model back to DTO
            var newRegionDto = new RegionGetDTO()
            {
                Id = region.Id,
                Name = region.Name,
                Code = region.Code,
                RegionImageUrl = region.RegionImageUrl,
            };

            return CreatedAtAction(nameof(GetRegionById), new {id = newRegionDto.Id}, newRegionDto); 
        }

        // UPDATE EXISTING REGION
        [HttpPut]
        [Route("{id}")]
        public IActionResult UpdateRegion([FromRoute] Guid id, [FromBody] RegionPutDTO regionPutDTO)
        {
            var region = dbContext.Regions.FirstOrDefault(x => x.Id == id);

            if(region == null)
            {
                return NotFound();
            }

            // Map DTO to Domain Model
            region.RegionImageUrl = regionPutDTO.RegionImageUrl;
            region.Name = regionPutDTO.Name;
            region.Code = regionPutDTO.Code;

            dbContext.SaveChanges();

            // Convert Domain Model back to DTO
            var regionDTO = new RegionGetDTO()
            {
                Id= region.Id,
                Name = region.Name,
                Code = region.Code,
                RegionImageUrl = region.RegionImageUrl,
            };

            return Ok(regionDTO);
        }

        // DELETE REGION
        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteRegion([FromRoute] Guid id)
        {
            var region = dbContext.Regions.FirstOrDefault(x => x.Id == id);

            if (region == null)
            {
                return NotFound();
            }

            dbContext.Regions.Remove(region);
            dbContext.SaveChanges();

            return Ok();
        }
    }
}
