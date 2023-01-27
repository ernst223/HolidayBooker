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
    public class PublicController: Controller
    {
        private PublicRepository publicRepository;
        public PublicController(MyContext T)
        {
            publicRepository = new PublicRepository(T);
        }

        [HttpGet("cleanup")]
        public IActionResult cleanup()
        {
            publicRepository.CleanVacations();
            return Ok();
        }

        [HttpPost("stock/filter")]
        public IActionResult getFilteredValues([FromBody]PublicFilterDto T)
        {
            var data = publicRepository.GetFilteredStock(T);
            if(data != null)
            {
                return Ok(data);
            }
            else
            {
                return BadRequest(data);
            }
        }

        [HttpPost("stock/fastfilter")]
        public IActionResult getFastFilteredValues([FromBody]FastFilterDto T)
        {
            var data = publicRepository.GetFastFilteredStock(T);
            if (data != null)
            {
                return Ok(data);
            }
            else
            {
                return BadRequest(data);
            }
        }

        [HttpGet("stock/all")]
        public IActionResult getAllStock()
        {
            var data = publicRepository.GetAllStock();
            if(data != null)
            {
                return Ok(data);
            }
            else
            {
                return BadRequest(data);
            }
        }

        [HttpPost("stock/filterpartner")]
        public IActionResult getFilteredValuesPartner([FromBody] PublicFilterDto T)
        {
            var data = publicRepository.GetFilteredStockPartner(T);
            if (data != null)
            {
                return Ok(data);
            }
            else
            {
                return BadRequest(data);
            }
        }

        [HttpPost("stock/fastfilterpartner")]
        public IActionResult getFastFilteredValuesPartner([FromBody] FastFilterDto T)
        {
            var data = publicRepository.GetFastFilteredStockPartner(T);
            if (data != null)
            {
                return Ok(data);
            }
            else
            {
                return BadRequest(data);
            }
        }

        [HttpGet("stock/allpartner")]
        public IActionResult getAllStockPartner()
        {
            var data = publicRepository.GetAllStockPartner();
            if (data != null)
            {
                return Ok(data);
            }
            else
            {
                return BadRequest(data);
            }
        }
    }
}
