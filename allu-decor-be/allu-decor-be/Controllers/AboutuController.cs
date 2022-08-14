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
    public class AboutuController : ControllerBase
    {
        private IAboutuService _aboutuService;

        public AboutuController(IAboutuService aboutuService)
        {
            _aboutuService = aboutuService;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult GetAll()
        {
            var aboutus = _aboutuService.GetAll();
            return Ok(new { status = "ok", message = "", responseObject = aboutus });
        }

        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            Aboutu aboutu;
            try
            {
                aboutu = _aboutuService.GetById(id);
            }
            catch (KeyNotFoundException e)
            {
                return Ok(new { status = "fail", message = e.Message, data = "" });
            }

            return Ok(new { status = "ok", message = "", responseObject = aboutu });
        }

        [HttpPost("CreateAboutu")]
        public IActionResult Create(Aboutu aboutu)
        {
            _aboutuService.CreateAboutu(aboutu);
            return Ok(new { status = "ok", message = "", responseObject = "" });
        }

        [HttpPost("UpdateAboutu")]
        public IActionResult Update(Aboutu aboutu)
        {
            try
            {
                _aboutuService.UpdateAboutu(aboutu);
            }
            catch (KeyNotFoundException e)
            {
                return Ok(new { status = "fail", message = e.Message, responseObject = "" });
            }
            return Ok(new { status = "ok", message = "", responseObject = "" });
        }

        [HttpPost("DeleteAboutu")]
        public IActionResult Delete(IdRequest idRequest)
        {
            _aboutuService.DeleteAboutu(idRequest.Id);
            return Ok(new { status = "ok", message = "", responseObject = "" });
        }
    }
}
