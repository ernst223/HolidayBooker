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
    public class ResortController: Controller
    {
        private ResortRepository resortRepository;
        public ResortController(MyContext T)
        {
            resortRepository = new ResortRepository(T);
        }

        [Authorize]
        [HttpGet("resortwithuserid/{userId}")]
        public IActionResult getResort(string userId)
        {
            return Ok(resortRepository.GetResorts(userId));
        }

        [Authorize]
        [HttpGet("resortwithregionwithuserid/{userId}")]
        public IActionResult getResortWithRegion(string userId)
        {
            return Ok(resortRepository.GetResortWithRegion(userId));
        }

        [Authorize]
        [HttpGet("resort/{id}")]
        public IActionResult getResort(int id)
        {
            return Ok(resortRepository.GetResort(id));
        }

        [HttpGet("resortwithuserid/last/{userId}")]
        public IActionResult getLatestResort(string userId)
        {
            return Ok(resortRepository.lastResort(userId));
        }

        [Authorize]
        [HttpPost("resortwithuserid/add/{userId}")]
        public IActionResult addResort([FromBody] Resort T, string userId)
        {
            return Ok(resortRepository.addResort(T, userId));
        }

        [Authorize]
        [HttpPut("resort/edit")]
        public IActionResult editResort([FromBody] Resort T)
        {
            return Ok(resortRepository.EditResort(T));
        }

        [Authorize]
        [HttpDelete("resort/delete/{id}")]
        public IActionResult deleteResort(int id)
        {
            return Ok(resortRepository.DeleteResort(id));
        }
    }
}
