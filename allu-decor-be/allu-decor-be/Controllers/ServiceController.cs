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
    public class ServiceController : ControllerBase
    {
        private IServiceService _service;
        public ServiceController(IServiceService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var services = _service.GetAll();
            return Ok(new { status = "ok", message = "", responseObject = services });
        }

        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            Service service;
            try
            {
                service = _service.GetById(id);
            }
            catch (KeyNotFoundException e)
            {
                return Ok(new { status = "fail", message = e.Message, data = "" });
            }

            return Ok(new { status = "ok", message = "", responseObject = service });
        }

        [HttpPost("CreateService")]
        public IActionResult Create(Service service)
        {
            _service.CreateService(service);
            return Ok(new { status = "ok", message = "", responseObject = "" });
        }

        [HttpPost("UpdateService")]
        public IActionResult Update(Service service)
        {
            try
            {
                _service.UpdateService(service);
            }
            catch (KeyNotFoundException e)
            {
                return Ok(new { status = "fail", message = e.Message, responseObject = "" });
            }
            return Ok(new { status = "ok", message = "", responseObject = "" });
        }

        [HttpPost("DeleteService")]
        public IActionResult Delete(IdRequest idRequest)
        {
            _service.DeleteService(idRequest.Id);
            return Ok(new { status = "ok", message = "", responseObject = "" });
        }
    }
}
