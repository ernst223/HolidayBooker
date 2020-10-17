using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vacation_Booker.Entities;
using Vacation_Booker.Repository;

namespace Vacation_Booker.Controllers
{
    [Route("api")]
    public class SupplierController: Controller
    {
        private SupplierRepository supplierRepository;
        public SupplierController(MyContext T)
        {
            supplierRepository = new SupplierRepository(T);
        }

        [Authorize]
        [HttpGet("supplier")]
        public IActionResult getSuppliers()
        {
            return Ok(supplierRepository.GetSuppliers());
        }

        [Authorize]
        [HttpGet("supplier/{id}")]
        public IActionResult getSuppliers(int id)
        {
            return Ok(supplierRepository.GetSupplier(id));
        }

        [Authorize]
        [HttpDelete("supplier/delete/{id}")]
        public IActionResult deleteSupplier(int id)
        {
            return Ok(supplierRepository.deleteSupplier(id));
        }

        [Authorize]
        [HttpPost("supplier/add")]
        public IActionResult addSupplier([FromBody] Supplier T)
        {
            return Ok(supplierRepository.AddSupplier(T));
        }

        [Authorize]
        [HttpPut("supplier/edit")]
        public IActionResult editSupplier([FromBody] Supplier T)
        {
            return Ok(supplierRepository.EditSupplier(T));
        }
    }
}
