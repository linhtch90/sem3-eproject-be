using allu_decor_be.Authorization;
using allu_decor_be.Models;
using allu_decor_be.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace allu_decor_be.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ContactinfoController : ControllerBase
    {
        private IContactinfoService _contactinfoService;

        public ContactinfoController(IContactinfoService contactinfoService)
        {
            _contactinfoService = contactinfoService;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult GetAll()
        {
            var contactInfos = _contactinfoService.GetAll();
            return Ok(new { status = "ok", message = "", responseObject = contactInfos });
        }


        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            Contactinfo contact;
            try
            {
                contact = _contactinfoService.GetById(id);
            }
            catch (KeyNotFoundException e)
            {
                return Ok(new { status = "fail", message = e.Message, data = "" });
            }

            return Ok(new { status = "ok", message = "", responseObject = contact });
        }

        [HttpPost("CreateContactInfo")]
        public IActionResult Create(Contactinfo contact)
        {
            _contactinfoService.CreateContactInfo(contact);
            return Ok(new { status = "ok", message = "", responseObject = "" });
        }

        [HttpPost("UpdateContactInfo")]
        public IActionResult Update(Contactinfo contact)
        {
            try
            {
                _contactinfoService.UpdateContactInfo(contact);
            }
            catch (KeyNotFoundException e)
            {
                return Ok(new { status = "fail", message = e.Message, responseObject = "" });
            }
            return Ok(new { status = "ok", message = "", responseObject = "" });
        }

        [HttpPost("DeleteContactInfo")]
        public IActionResult Delete(IdRequest idRequest)
        {
            _contactinfoService.DeleteContactInfo(idRequest.Id);
            return Ok(new { status = "ok", message = "", responseObject = "" });
        }
    }
}
