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
    public class UnitSizeController: Controller
    {
        private UnitSizeRepository unitSizeRepository;
        public UnitSizeController(MyContext T)
        {
            unitSizeRepository = new UnitSizeRepository(T);
        }
        [HttpGet("unitsize")]
        public IActionResult getUnitSizes()
        {
            return Ok(unitSizeRepository.GetUnitSizes());
        }

        [HttpGet("unitsize/{id}")]
        public IActionResult getUnitSize(int id)
        {
            return Ok(unitSizeRepository.GetUnitSize(id));
        }

        [Authorize]
        [HttpPost("unitsize/add")]
        public IActionResult addUnitSizes([FromBody] UnitSizes T)
        {
            return Ok(unitSizeRepository.AddUnitSize(T));
        }

        [Authorize]
        [HttpPut("unitsize/edit")]
        public IActionResult editUnitSizes([FromBody] UnitSizes T)
        {
            return Ok(unitSizeRepository.EditUnitSize(T));
        }

        [Authorize]
        [HttpDelete("unitsize/delete/{id}")]
        public IActionResult deleteUnitSizes(int id)
        {
            return Ok(unitSizeRepository.DeleteUnitSize(id));
        }
    }
}
