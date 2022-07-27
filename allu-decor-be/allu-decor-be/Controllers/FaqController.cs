using allu_decor_be.Models;
using allu_decor_be.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace allu_decor_be.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FaqController : ControllerBase
    {
        private IFaqService _faq;

        public FaqController(IFaqService faq)
        {
            _faq = faq;
        }   

        [HttpGet]
        public IActionResult GetAll()
        {
            var faqs = _faq.GetAll();
            return Ok(new { status = "ok", message = "", responseObject = faqs });

        }

        [HttpGet("id")]
        public IActionResult GetById(string id)
        {
            Faq faq;
            try
            {
                faq = _faq.GetById(id);
            }
            catch (KeyNotFoundException e)
            {
                return Ok(new { status = "fail", message = e.Message, data = "" });
            }

            return Ok(new { status = "ok", message = "", responseObject = faq });
        }

        [HttpPost("CreateFaq")]
        public IActionResult CreateFaq(Faq faq)
        {
            _faq.CreateFaq(faq);
            return Ok(new { status = "ok", message = "", responseObject = "" });
        }

        [HttpPut("UpdateFaq")]
        public IActionResult UpdateFaq(Faq faq)
        {
            try
            {
                _faq.UpdateFaq(faq);
            }
            catch (KeyNotFoundException e)
            {
                return Ok(new { status = "fail", message = e.Message, responseObject = "" });
            }
            return Ok(new { status = "ok", message = "", responseObject = "" });
        }

        [HttpPost("DeleteFaq")]
        public IActionResult DeleteFaq(IdRequest idRequest)
        {
            _faq.DeleteFaq(idRequest.Id);
            return Ok(new { status = "ok", message = "", responseObject = "" });
        }


    }
}
