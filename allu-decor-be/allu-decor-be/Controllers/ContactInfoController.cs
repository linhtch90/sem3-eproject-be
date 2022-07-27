using allu_decor_be.Models;
using allu_decor_be.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace allu_decor_be.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactInfoController : ControllerBase
    {
        private IContactInfoService _contact;

        public ContactInfoController(IContactInfoService contact)
        {
            _contact = contact;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var contactInfos = _contact.GetAll();
            return Ok(new { status = "ok", message = "", responseObject = contactInfos });
        }


        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            Contactinfo contact;
            try
            {
                contact = _contact.GetById(id);
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
            _contact.CreateContactInfo(contact);
            return Ok(new { status = "ok", message = "", responseObject = "" });
        }

        [HttpPut("UpdateContactInfo")]
        public IActionResult Update(Contactinfo contact)
        {
            try
            {
                _contact.UpdateContactInfo(contact);
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
            _contact.DeleteContactInfo(idRequest.Id);
            return Ok(new { status = "ok", message = "", responseObject = "" });
        }

    }
}
