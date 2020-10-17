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
    public class DummyController: Controller
    {
        private DummyRepository dummyRepository;
        public DummyController(MyContext T)
        {
            dummyRepository = new DummyRepository(T);
        }
        [HttpGet("dummy")]
        public IActionResult testDatabaseGet()

        {
            return Ok(dummyRepository.getResort());
        }

        [HttpPost("dummy/create")]
        public IActionResult testDatabasePost(string username, string password)
        {
            return Ok(dummyRepository.addUser("Dummy","Dummy"));
        }
    }
}
