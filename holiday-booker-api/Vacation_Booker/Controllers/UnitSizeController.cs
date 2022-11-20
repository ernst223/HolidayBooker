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
        [HttpGet("unitsizewithuserid/{userId}")]
        public IActionResult getUnitSizes(string userId)
        {
            return Ok(unitSizeRepository.GetUnitSizes(userId));
        }

        [HttpGet("unitsize/{id}")]
        public IActionResult getUnitSize(int id)
        {
            return Ok(unitSizeRepository.GetUnitSize(id));
        }

        [Authorize]
        [HttpPost("unitsizewithuserid/add/{userId}")]
        public IActionResult addUnitSizes([FromBody] UnitSizes T, string userId)
        {
            return Ok(unitSizeRepository.AddUnitSize(T, userId));
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
