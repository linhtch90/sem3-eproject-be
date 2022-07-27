using allu_decor_be.Models;
using allu_decor_be.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace allu_decor_be.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private IFeedbackSevice _feedback;


        public FeedbackController(IFeedbackSevice feedback)
        {
            _feedback = feedback;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var feedback = _feedback.GetAll();
            return Ok(new { status = "ok", message = "", responseObject = feedback });
        }


        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            Feedback feddback;
            try
            {
                feddback = _feedback.GetById(id);
            }
            catch (KeyNotFoundException e)
            {
                return Ok(new { status = "fail", message = e.Message, data = "" });
            }

            return Ok(new { status = "ok", message = "", responseObject = feddback });
        }

        [HttpPost("CreateFeedback")]
        public IActionResult Create(Feedback feedback)
        {
            _feedback.CreateFeedback(feedback);
            return Ok(new { status = "ok", message = "", responseObject = "" });
        }

        [HttpPut("UpdateFeedback")]
        public IActionResult Update(Feedback feedback)
        {
            try
            {
                _feedback.UpdateFeedback(feedback);
            }
            catch (KeyNotFoundException e)
            {
                return Ok(new { status = "fail", message = e.Message, responseObject = "" });
            }
            return Ok(new { status = "ok", message = "", responseObject = "" });
        }

        [HttpPost("DeleteFeedback")]
        public IActionResult Delete(IdRequest idRequest)
        {
            _feedback.DeleteFeedback(idRequest.Id);
            return Ok(new { status = "ok", message = "", responseObject = "" });
        }

    }
}
