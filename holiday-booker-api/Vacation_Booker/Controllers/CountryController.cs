using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Vacation_Booker.Entities;
using Vacation_Booker.Models;
using Vacation_Booker.Repository;

namespace Vacation_Booker.Controllers
{
    [Route("api")]
    public class CountryController: Controller
    {
        private CountryRepository countryRepository;
        private ExportRepository exportRepository;
        public CountryController(MyContext T)
        {
            countryRepository = new CountryRepository(T);
            exportRepository = new ExportRepository(T);
        }

        [HttpGet("country")]
        public IActionResult getCountries()

        {
            return Ok(countryRepository.GetCountries());
        }

        [HttpGet("countrywithuserid/{userId}")]
        public IActionResult getCountries(string userId)
        
        {
            return Ok(countryRepository.GetCountries(userId));
        }

        [HttpGet("exportToExcel64Getwithuserid/{SupplierId}/{ResortId}/{TheDate}/{userId}")]
        public IActionResult ExportToExcel64(int SupplierId, int ResortId, string TheDate, string userId)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                FilterStock temp = new FilterStock();
                if(TheDate == "null")
                {
                    temp.TheDate = null;
                } else
                {
                    temp.TheDate = Convert.ToDateTime(TheDate);
                }
                temp.ResortId = ResortId;
                temp.SupplierId = SupplierId;
                var stream = exportRepository.GenerateReport(temp, userId);
                stream.CopyTo(ms);
                var file = File(ms.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");

                file.FileDownloadName = $"Report-{DateTime.Now.ToShortDateString()}.xlsx";

                Byte[] bytes = file.FileContents;
                string result = Convert.ToBase64String(bytes);
                return Ok(new ExportBase64(result));
            }
        }

        [Authorize]
        [HttpGet("country/{id}")]
        public IActionResult getCountry(int id)
        {
            return Ok(countryRepository.GetCountry(id));
        }

        [Authorize]
        [HttpPost("countrywithuserid/add/{userId}")]
        public IActionResult addCountry([FromBody] Country T, string userId)
        {
            return Ok(countryRepository.AddCountry(T, userId));
        }

        [Authorize]
        [HttpPut("country/edit")]
        public IActionResult editCountry([FromBody] Country T)
        {
            return Ok(countryRepository.EditCountry(T));
        }

        [Authorize]
        [HttpDelete("country/delete/{id}")]
        public IActionResult deleteCountry(int id)
        {
            return Ok(countryRepository.DeleteCountry(id));
        }
    }
}
