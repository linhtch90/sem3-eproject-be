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
    public class DomainserviceController : ControllerBase
    {
        private IDomainserviceService _domainserviceService;

        public DomainserviceController(IDomainserviceService domainserviceService)
        {
            _domainserviceService = domainserviceService;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var domainservices = _domainserviceService.GetAll();
            return Ok(new { status = "ok", message = "", responseObject = domainservices });
        }

        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            Domainservice domainservice;
            try
            {
                domainservice = _domainserviceService.GetById(id);
            }
            catch (KeyNotFoundException e)
            {
                return Ok(new { status = "fail", message = e.Message, data = "" });
            }

            return Ok(new { status = "ok", message = "", responseObject = domainservice });
        }

        [HttpPost("CreateDomain")]
        public IActionResult Create(Domainservice domainservice)
        {
            _domainserviceService.CreateDomainservice(domainservice);
            return Ok(new { status = "ok", message = "", responseObject = "" });
        }

        [HttpPost("UpdateDomain")]
        public IActionResult Update(Domainservice domainservice)
        {
            try
            {
                _domainserviceService.UpdateDomainservice(domainservice);
            }
            catch (KeyNotFoundException e)
            {
                return Ok(new { status = "fail", message = e.Message, responseObject = "" });
            }
            return Ok(new { status = "ok", message = "", responseObject = "" });
        }

        [HttpPost("DeleteDomain")]
        public IActionResult Delete(IdRequest idRequest)
        {
            _domainserviceService.DeleteDomainservice(idRequest.Id);
            return Ok(new { status = "ok", message = "", responseObject = "" });
        }
    }
}
