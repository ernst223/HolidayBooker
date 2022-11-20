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
    public class AreaController: Controller
    {
        private AreaRepository areaRepository;
        public AreaController(MyContext T)
        {
            areaRepository = new AreaRepository(T);
        }

        [Authorize]
        [HttpGet("areawithuserid/{userId}")]
        public IActionResult getAreas(string userId)
        {
            return Ok(areaRepository.GetAreas(userId));
        }

        [Authorize]
        [HttpGet("areadtowithuserid/{userId}")]
        public IActionResult getAreasDto(string userId)
        {
            return Ok(areaRepository.GetAreasDto(userId));
        }

        [Authorize]
        [HttpGet("area/{id}")]
        public IActionResult getArea(int id)
        {
            return Ok(areaRepository.GetArea(id));
        }

        [HttpGet("areapercountry/{id}")]
        public IActionResult getAreaPerCountry(int id)
        {
            return Ok(areaRepository.getAreaPerCountry(id));
        }

        [Authorize]
        [HttpPost("areawithuserid/add/{userId}")]
        public IActionResult addArea([FromBody] Area T, string userId)
        {
            return Ok(areaRepository.addArea(T, userId));
        }

        [Authorize]
        [HttpPut("area/edit")]
        public IActionResult editArea([FromBody] Area T)
        {
            return Ok(areaRepository.updateArea(T));
        }

        [Authorize]
        [HttpDelete("area/delete/{id}")]
        public IActionResult deleteArea(int id)
        {
            return Ok(areaRepository.DeleteArea(id));
        }
    }
}
