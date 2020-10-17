using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vacation_Booker.Entities;
using Vacation_Booker.Models;
using Vacation_Booker.Repository;

namespace Vacation_Booker.Controllers
{
    [Route("api")]
    public class VacationController: Controller
    {
        private VacationRepository vacationRepository;
        public VacationController(MyContext T)
        {
            vacationRepository = new VacationRepository(T);
        }

        [HttpGet("stockwithdetails")]
        public IActionResult getStockWithDetails()
        {
            var result = vacationRepository.getStockWithDetails();
            if(result != null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest("Soz man something went wrong");
            }
        }

        [Authorize]
        [HttpGet("vacation")]
        public IActionResult getVacations()
        {
            return Ok(vacationRepository.GetVacations());
        }

        [Authorize]
        [HttpPost("checkforduplicates")]
        public IActionResult checkForDuplicates([FromBody] ListOfVacationDto T)
        {
            return Ok(vacationRepository.checkforduplicates(T.myList));
        }

        [Authorize]
        [HttpGet("vacationfordisplay")]
        public IActionResult getVacationsForDisplay()
        {
            return Ok(vacationRepository.GetVacationDisplay());
        }

        [Authorize]
        [HttpPost("filtervacation")]
        public IActionResult getFilteredVacation([FromBody] FilterStock T)
        {
            return Ok(vacationRepository.getFilteredVacation(T));
        }

        [Authorize]
        [HttpGet("filterbystate/{filter}")]
        public IActionResult getFilterbyState(int filter)
        {
            return Ok(vacationRepository.getFilterByState(filter));
        }

        [Authorize]
        [HttpGet("vacation/{id}")]
        public IActionResult getVacation(int id)
        {
            return Ok(vacationRepository.GetVacation(id));
        }

        [Authorize]
        [HttpDelete("vacation/delete/{id}")]
        public IActionResult deleteVacation(int id)
        {
            return Ok(vacationRepository.DeleteVacation(id));
        }

        [Authorize]
        [HttpPost("vacation/add")]
        public IActionResult addVacation([FromBody] Vacation T)
        {
            return Ok(vacationRepository.AddVacation(T));
        }

        [Authorize]
        [HttpPost("vacation/addlist")]
        public IActionResult addVacationlist([FromBody] VacationList T)
        {
            return Ok(vacationRepository.AddVacationList(T.myList));
        }

        [Authorize]
        [HttpPut("vacation/edit")]
        public IActionResult editVacation([FromBody] Vacation T)
        {
            return Ok(vacationRepository.EditVacation(T));
        }

        [Authorize]
        [HttpPut("vacation/edit/sold")]
        public IActionResult editVacationSold([FromBody] Vacation T)
        {
            return Ok(vacationRepository.EditVacationSold(T));
        }

        [Authorize]
        [HttpPut("vacation/edit/hold")]
        public IActionResult editVacationHold([FromBody] Vacation T)
        {
            return Ok(vacationRepository.EditVacationHold(T));
        }

        [Authorize]
        [HttpPost("getduplicatesfordisplay")]
        public IActionResult getDuplicatesForDisplay([FromBody] List<Vacation> T)
        {
            return Ok(vacationRepository.getDisplayDuplicates(T));
        }

        [Authorize]
        [HttpPost("upload/vacation/csv/{supplier}/{adminFee}")]
        public IActionResult uploadVacationCSV(IFormFile file, string supplier, int adminFee)
        {
            return Ok(vacationRepository.uploadVactionCSV(file, supplier, adminFee));
        }
    }
}
