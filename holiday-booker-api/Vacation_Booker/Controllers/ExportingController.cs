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
    public class ExportingController: Controller
    {
        private ExportRepository exportRepository;
        public ExportingController(MyContext T)
        {
            exportRepository = new ExportRepository(T);
        }

        //[Authorize]
        [HttpPost("exportToExcel")]
        public IActionResult ExportToExcel([FromBody] FilterStock T)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                var stream = exportRepository.GenerateReport(T);
                stream.CopyTo(ms);
                var file = File(ms.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");

                file.FileDownloadName = $"Report-{DateTime.Now.ToShortDateString()}.xlsx";

                return file;
            }
        }

        [HttpPost("exportToExcel64")]
        public IActionResult ExportToExcel64([FromBody] FilterStock T)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                var stream = exportRepository.GenerateReport(T);
                stream.CopyTo(ms);
                var file = File(ms.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");

                file.FileDownloadName = $"Report-{DateTime.Now.ToShortDateString()}.xlsx";

                Byte[] bytes = file.FileContents;
                string result = Convert.ToBase64String(bytes);
                return Ok(new ExportBase64(result));
            }
        }
    }

    public class ExportBase64
    {
        public ExportBase64(string p) {
            Body = p;
        }
        public string Body { get; set; }
    }
}
