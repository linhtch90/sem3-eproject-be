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
    public class CustomerreviewController : ControllerBase
    {
        private ICustomerReviewService _customerReviewService;

        public CustomerreviewController(ICustomerReviewService customerReviewService)
        {
            _customerReviewService = customerReviewService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var customerreviews = _customerReviewService.GetAll();
            return Ok(new { status = "ok", message = "", responseObject = customerreviews });
        }

        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            Customerreview customerreview;
            try
            {
                customerreview = _customerReviewService.GetById(id);
            }
            catch (KeyNotFoundException e)
            {
                return Ok(new { status = "fail", message = e.Message, data = "" });
            }

            return Ok(new { status = "ok", message = "", responseObject = customerreview });
        }

        [HttpPost("CreateCustomerreview")]
        public IActionResult Create(Customerreview customerreview)
        {
            _customerReviewService.CreateCustomerreview(customerreview);
            return Ok(new { status = "ok", message = "", responseObject = "" });
        }

        [HttpPost("UpdateCustomerreview")]
        public IActionResult Update(Customerreview customerreview)
        {
            try
            {
                _customerReviewService.UpdateCustomerreview(customerreview);
            }
            catch (KeyNotFoundException e)
            {
                return Ok(new { status = "fail", message = e.Message, responseObject = "" });
            }
            return Ok(new { status = "ok", message = "", responseObject = "" });
        }

        [HttpPost("DeleteCustomerreview")]
        public IActionResult Delete(IdRequest idRequest)
        {
            _customerReviewService.DeleteCustomerreview(idRequest.Id);
            return Ok(new { status = "ok", message = "", responseObject = "" });
        }
    }
}
