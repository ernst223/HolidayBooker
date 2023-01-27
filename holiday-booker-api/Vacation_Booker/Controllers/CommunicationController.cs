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
    public class CommunicationController: Controller
    {
        private CommunicationRepository communicationRepository;
        public CommunicationController(MyContext T)
        {
            communicationRepository = new CommunicationRepository(T);
        }

        [HttpPost("enquiry")]
        public IActionResult Enquiry([FromBody]EnquiryDto T)
        {
            if (communicationRepository.Enquiry(T))
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpPost("enquirypartner")]
        public IActionResult EnquiryPartner([FromBody] EnquiryDto T)
        {
            if (communicationRepository.EnquiryPartner(T))
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet("testInvoiceTable/{id}")]
        public IActionResult testInvoiceTable(int id)
        {
            return Ok(communicationRepository.addInvoice(id));
        }

        [HttpGet("newClient/{email}")]
        public IActionResult NewEmail(string email)
        {
            if (communicationRepository.NewEmailSubmitted(email))
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet("newClientPartner/{email}")]
        public IActionResult NewEmailPartner(string email)
        {
            if (communicationRepository.NewEmailSubmittedPartner(email))
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet("invoice/{id}/{email}/{name}/{lastname}/{dob}/{cell}/{kids}/{adults}")]
        public IActionResult InvoiceClient(int id, string email, string name, string lastname, string dob, string cell, int kids, int adults)
        {
            if (communicationRepository.InvoiceClient(id, email, name, lastname, dob, cell, kids, adults))
            {
                return Ok("Client contacted and Emailed successfully!  If you press the Available button again the Client will be Emailed again.");
            }
            else
            {
                return BadRequest("Something went wrong. Did not contact the Client!");
            }
        }

        [HttpGet("invoice/partner/{id}/{email}/{name}/{lastname}/{dob}/{cell}/{kids}/{adults}")]
        public IActionResult InvoiceClientPartner(int id, string email, string name, string lastname, string dob, string cell, int kids, int adults)
        {
            if (communicationRepository.InvoiceClientPartner(id, email, name, lastname, dob, cell, kids, adults))
            {
                return Ok("Client contacted and Emailed successfully!  If you press the Available button again the Client will be Emailed again.");
            }
            else
            {
                return BadRequest("Something went wrong. Did not contact the Client!");
            }
        }

        [HttpGet("notify/{id}/{email}/{name}/{lastname}")]
        public IActionResult NotifyClient(int id, string email, string name, string lastname)
        {
            if (communicationRepository.NotifyClient(id, email, name, lastname))
            {
                return Ok("Client notified and Emailed successfully! If you press the Sold Out button again the Client will be Emailed again.");
            }
            else
            {
                return BadRequest("Something went wrong. Did not notify the Client!");
            }
        }

        [HttpGet("notify/partner/{id}/{email}/{name}/{lastname}")]
        public IActionResult NotifyClientPartner(int id, string email, string name, string lastname)
        {
            if (communicationRepository.NotifyClientPartner(id, email, name, lastname))
            {
                return Ok("Client notified and Emailed successfully! If you press the Sold Out button again the Client will be Emailed again.");
            }
            else
            {
                return BadRequest("Something went wrong. Did not notify the Client!");
            }
        }

        [HttpGet("dummyenquiry")]
        public IActionResult DummyEnquiry()
        {
            EnquiryDto T = new EnquiryDto()
            {
                Name = "DummyName",
                Lastname = "DummyLastname",
                Dob = "Test Date",
                Cell = "0824189002",
                Email = "ernst.blignaut0@gmail.com",
                StockId = 35
            };
            if (communicationRepository.Enquiry(T))
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
