using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vacation_Booker.Entities;
using Vacation_Booker.Repository;

namespace Vacation_Booker.Controllers
{
    [Route("api")]
    public class RegionController: Controller
    {
        private RegionRepository regionRepository;
        public RegionController(MyContext T)
        {
            regionRepository = new RegionRepository(T);
        }
        [HttpGet("regionwithuserid/{userId}")]
        public IActionResult getRegions(string userId)
        {
            return Ok(regionRepository.GetRegions(userId));
        }

        [Authorize]
        [HttpGet("regiondtowithuserid/{userId}")]
        public IActionResult getRegionsDto(string userId)
        {
            return Ok(regionRepository.GetRegionsDto(userId));
        }

        [Authorize]
        [HttpGet("region/{id}")]
        public IActionResult getRegion(int id)
        {
            return Ok(regionRepository.GetRegion(id));
        }

        [Authorize]
        [HttpPost("regionwithuserid/add/{userId}")]
        public IActionResult addRegion([FromBody] Region T, string userId)
        {
            return Ok(regionRepository.AddRegion(T, userId));
        }

        [Authorize]
        [HttpPut("region/edit")]
        public IActionResult editRegion([FromBody] Region T)
        {
            return Ok(regionRepository.EditRegion(T));
        }

        [Authorize]
        [HttpDelete("region/delete/{id}")]
        public IActionResult deleteRegion(int id)
        {
            return Ok(regionRepository.DeleteRegion(id));
        }
    }
}
