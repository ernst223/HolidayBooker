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
    public class VacationSuppliersController: Controller
    {
        private VacationSupplierRepository vacationSupplierRepository;
        public VacationSuppliersController(MyContext T)
        {
            vacationSupplierRepository = new VacationSupplierRepository(T);
        }
        [HttpGet("vacationsuppliers")]
        public IActionResult getVacationSuppliers()
        {
            return Ok(vacationSupplierRepository.GetVacationSuppliers());
        }

        [HttpGet("vacationsuppliers/{id}")]
        public IActionResult getVacationSupplier(int id)
        {
            return Ok(vacationSupplierRepository.GetVacationSupplier(id));
        }

        [Authorize]
        [HttpPost("vacationsuppliers/add")]
        public IActionResult addVacationSuppliers([FromBody] VacationSuppliers T)
        {
            return Ok(vacationSupplierRepository.AddVacationSupplier(T));
        }

        [Authorize]
        [HttpPut("vacationsuppliers/edit")]
        public IActionResult editVacationSuppliers([FromBody] VacationSuppliers T)
        {
            return Ok(vacationSupplierRepository.EditVacationSupplier(T));
        }

        [Authorize]
        [HttpDelete("vacationsuppliers/delete/{id}")]
        public IActionResult deleteVacationSuppliers(int id)
        {
            return Ok(vacationSupplierRepository.DeleteVacationSupplier(id));
        }
    }
}
