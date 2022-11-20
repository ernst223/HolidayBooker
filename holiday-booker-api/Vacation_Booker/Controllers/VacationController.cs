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

        [HttpGet("stockwithdetailswithuserid/{userId}")]
        public IActionResult getStockWithDetails(string userId)
        {
            var result = vacationRepository.getStockWithDetails(userId);
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
        [HttpGet("vacationwithuserid/{userId}")]
        public IActionResult getVacations(string userId)
        {
            return Ok(vacationRepository.GetVacations(userId));
        }

        [Authorize]
        [HttpPost("checkforduplicates")]
        public IActionResult checkForDuplicates([FromBody] ListOfVacationDto T)
        {
            return Ok(vacationRepository.checkforduplicates(T.myList));
        }

        [Authorize]
        [HttpGet("vacationfordisplaywithuserid/{userId}")]
        public IActionResult getVacationsForDisplay(string userId)
        {
            return Ok(vacationRepository.GetVacationDisplay(userId));
        }

        [Authorize]
        [HttpPost("filtervacationwithuserid/{userId}")]
        public IActionResult getFilteredVacation([FromBody] FilterStock T, string userId)
        {
            return Ok(vacationRepository.getFilteredVacation(T, userId));
        }

        [Authorize]
        [HttpGet("filterbystatewithuserid/{filter}/{userId}")]
        public IActionResult getFilterbyState(int filter, string userId)
        {
            return Ok(vacationRepository.getFilterByState(filter, userId));
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
        [HttpDelete("vacation/deletebyproviderid/{id}")]
        public IActionResult deleteVacationByProviderId(int id)
        {
            return Ok(vacationRepository.DeleteVacationdeleteVacationByProviderId(id));
        }

        [Authorize]
        [HttpPost("vacationwithuserid/add/{userId}")]
        public IActionResult addVacation([FromBody] Vacation T, string userId)
        {
            return Ok(vacationRepository.AddVacation(T, userId));
        }

        [Authorize]
        [HttpPost("vacationwithuserid/addlist/{userId}")]
        public IActionResult addVacationlist([FromBody] VacationList T, string userId)
        {
            return Ok(vacationRepository.AddVacationList(T.myList, userId));
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
        [HttpPost("uploadwithuserid/vacation/csv/{supplier}/{adminFee}/{userId}")]
        public IActionResult uploadVacationCSV(IFormFile file, string supplier, int adminFee, string userId)
        {
            return Ok(vacationRepository.uploadVactionCSV(file, supplier, adminFee, userId));
        }

        [Authorize]
        [HttpGet("getPartnersStockwithuserid/{userId}")]
        public IActionResult getPartnersStock(string userId)
        {
            return Ok(vacationRepository.getPartnersStock(userId));
        }

        [Authorize]
        [HttpGet("updatedefaultpartnerpricewithuserid/{price}/{userId}")]
        public IActionResult updateDefaultPartnerPrice(int price, string userId)
        {
            return Ok(vacationRepository.updateDefaultPartnerPrice(price, userId));
        }

        [Authorize]
        [HttpGet("updatepartnerstockprofit/{stockId}/{price}")]
        public IActionResult updatePartnerStockProfit(int stockId, int price)
        {
            return Ok(vacationRepository.updatePartnerStockProfit(stockId, price));
        }

        [Authorize]
        [HttpGet("getuserdefaultprofitwithuserid/{userId}")]
        public IActionResult getUserDefaultProfit(string userId)
        {
            return Ok(vacationRepository.getUserDefaultProfit(userId));
        }
    }
}
