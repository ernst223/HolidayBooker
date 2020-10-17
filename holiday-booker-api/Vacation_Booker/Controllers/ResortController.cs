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
        [HttpGet("resort")]
        public IActionResult getResort()
        {
            return Ok(resortRepository.GetResorts());
        }

        [Authorize]
        [HttpGet("resortwithregion")]
        public IActionResult getResortWithRegion()
        {
            return Ok(resortRepository.GetResortWithRegion());
        }

        [Authorize]
        [HttpGet("resort/{id}")]
        public IActionResult getResort(int id)
        {
            return Ok(resortRepository.GetResort(id));
        }

        [HttpGet("resort/last")]
        public IActionResult getLatestResort()
        {
            return Ok(resortRepository.lastResort());
        }

        [Authorize]
        [HttpPost("resort/add")]
        public IActionResult addResort([FromBody] Resort T)
        {
            return Ok(resortRepository.addResort(T));
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
