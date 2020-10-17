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
    public class ResortUnitsController: Controller
    {
        private ResortUnitsRepository resortUnitsRepository;
        public ResortUnitsController(MyContext T)
        {
            resortUnitsRepository = new ResortUnitsRepository(T);
        }
        [HttpGet("resortunits")]
        public IActionResult getResortUnits()
        {
            return Ok(resortUnitsRepository.GetResortUnits());
        }

        [HttpGet("resortunits/{id}")]
        public IActionResult getResortUnit(int id)
        {
            return Ok(resortUnitsRepository.GetResortUnit(id));
        }
        [HttpGet("unitsize/perresort/{id}")]
        public IActionResult getResortUnitperResort(int id)
        {
            return Ok(resortUnitsRepository.GetUnitSizesPerResort(id));
        }

        [Authorize]
        [HttpPost("resortunits/add")]
        public IActionResult addResortUnits([FromBody] ResortUnits T)
        {
            return Ok(resortUnitsRepository.AddResortUnit(T));
        }

        [Authorize]
        [HttpPut("resortunits/edit")]
        public IActionResult editResortUnits([FromBody] ResortUnits T)
        {
            return Ok(resortUnitsRepository.EditResortUnit(T));
        }

        [Authorize]
        [HttpDelete("resortunits/delete/{id}")]
        public IActionResult deleteResortUnits(int id)
        {
            return Ok(resortUnitsRepository.DeleteResortUnit(id));
        }

        [Authorize]
        [HttpDelete("resortunits/deletebyresort/{id}")]
        public IActionResult deleteResortUnitsByResort(int id)
        {
            return Ok(resortUnitsRepository.DeleteUnitSizeAtResortId(id));
        }

    }
}
