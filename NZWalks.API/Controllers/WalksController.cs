using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTOs;
using NZWalks.API.Services;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IMapper mapper;
        private IWalkService walkService;

        public WalksController(IMapper mapper, IWalkService walkService)
        {
            this.mapper = mapper;
            this.walkService = walkService;
        }

        // CREATE WALK
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] WalkPostDTO walkPostDTO)
        {
            // Map DTO to Domain Model
            Walk walk = mapper.Map<Walk>(walkPostDTO);

            walk = await walkService.CreateAsync(walk);

            return Ok(mapper.Map<WalkGetDTO>(walk));
        }

        // GET ALL WALKS
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var walks = await walkService.GetAllAsync();
            var walkDtos = mapper.Map<List<WalkGetDTO>>(walks);
            return Ok(walkDtos);
        }

        // GET WALK BY ID
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetWalkById([FromRoute] Guid id)
        {
            var walk = await walkService.GetWalkByIdAsync(id);

            if(walk == null)
            {
                return NotFound();
            }

            var walkDto = mapper.Map<WalkGetDTO>(walk);
            return Ok(walkDto);
        }

        // UPDATE WALK
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateWalk([FromBody] WalkPutDTO walkPutDto, [FromRoute] Guid id)
        {
            var walk = mapper.Map<Walk>(walkPutDto);

            walk = await walkService.UpdateWalkAsync(walk, id);

            if(walk == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<WalkGetDTO>(walk));
        }

        // DELETE WALK
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteWalk([FromRoute] Guid id)
        {
            var walk = await walkService.DeleteWalkAsync(id);

            if(walk == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<WalkGetDTO>(walk));
        }
    }
}
