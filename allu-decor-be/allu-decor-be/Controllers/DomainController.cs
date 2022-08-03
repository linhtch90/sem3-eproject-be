using allu_decor_be.Authorization;
using allu_decor_be.Models;
using allu_decor_be.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace allu_decor_be.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DomainController : ControllerBase
    {
        private IDomainService _domainService;

        public DomainController(IDomainService domainService)
        {
            _domainService = domainService;
        }
                
        [AllowAnonymous]
        [HttpGet]
        public IActionResult GetAll()
        {
            var domains = _domainService.GetAll();
            return Ok(new { status = "ok", message = "", responseObject = domains});
        }

        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            Domain domain;
            try
            {
                domain = _domainService.GetById(id);
            }
            catch (KeyNotFoundException e)
            {
                return Ok(new { status = "fail", message = e.Message, data = ""});
            }

            return Ok(new { status = "ok", message = "", responseObject = domain });
        }

        [HttpPost("CreateDomain")]
        public IActionResult Create(Domain domain)
        {
            _domainService.CreateDomain(domain);
            return Ok(new {status = "ok", message = "", responseObject = ""});
        }

        [HttpPost("UpdateDomain")]
        public IActionResult Update(Domain domain)
        {
            try
            {
                _domainService.UpdateDomain(domain);
            }
            catch(KeyNotFoundException e)
            {
                return Ok(new { status = "fail", message = e.Message, responseObject = "" });
            }
            return Ok(new { status = "ok", message = "", responseObject = "" });
        }

        [HttpPost("DeleteDomain")]
        public IActionResult Delete(IdRequest idRequest)
        {
            _domainService.DeleteDomain(idRequest.Id);
            return Ok(new { status = "ok", message = "", responseObject = "" });
        }
    }
}
