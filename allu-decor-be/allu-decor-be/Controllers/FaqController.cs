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
    public class FaqController : ControllerBase
    {
        private IFaqService _faqService;

        public FaqController(IFaqService faqService)
        {
            _faqService = faqService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var faqs = _faqService.GetAll();
            return Ok(new { status = "ok", message = "", responseObject = faqs });

        }

        [HttpGet("id")]
        public IActionResult GetById(string id)
        {
            Faq faq;
            try
            {
                faq = _faqService.GetById(id);
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
            _faqService.CreateFaq(faq);
            return Ok(new { status = "ok", message = "", responseObject = "" });
        }

        [HttpPost("UpdateFaq")]
        public IActionResult UpdateFaq(Faq faq)
        {
            try
            {
                _faqService.UpdateFaq(faq);
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
            _faqService.DeleteFaq(idRequest.Id);
            return Ok(new { status = "ok", message = "", responseObject = "" });
        }
    }
}
