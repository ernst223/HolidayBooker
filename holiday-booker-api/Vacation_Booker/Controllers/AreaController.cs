﻿using Microsoft.AspNetCore.Authorization;
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
        [HttpGet("area")]
        public IActionResult getAreas()
        {
            return Ok(areaRepository.GetAreas());
        }

        [Authorize]
        [HttpGet("areadto")]
        public IActionResult getAreasDto()
        {
            return Ok(areaRepository.GetAreasDto());
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
        [HttpPost("area/add")]
        public IActionResult addArea([FromBody] Area T)
        {
            return Ok(areaRepository.addArea(T));
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
