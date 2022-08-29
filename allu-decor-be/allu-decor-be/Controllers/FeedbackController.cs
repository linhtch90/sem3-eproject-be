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
    public class FeedbackController : ControllerBase
    {
        private IFeedbackSevice _feedbackService;


        public FeedbackController(IFeedbackSevice feedbackService)
        {
            _feedbackService = feedbackService;
        }

        [Authorize("Admin")]
        [HttpGet]
        public IActionResult GetAll()
        {
            var feedback = _feedbackService.GetAll();
            return Ok(new { status = "ok", message = "", responseObject = feedback });
        }

        [AllowAnonymous]
        [HttpPost("GetAllByProductId")]
        public IActionResult GetAllByProductId(IdRequest idRequest)
        {
            var feedback = _feedbackService.GetAllByProductId(idRequest.Id);
            return Ok(new { status = "ok", message = "", responseObject = feedback });
        }


        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            Feedback feddback;
            try
            {
                feddback = _feedbackService.GetById(id);
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
            _feedbackService.CreateFeedback(feedback);
            return Ok(new { status = "ok", message = "", responseObject = "" });
        }

        [HttpPost("UpdateFeedback")]
        public IActionResult Update(Feedback feedback)
        {
            try
            {
                _feedbackService.UpdateFeedback(feedback);
            }
            catch (KeyNotFoundException e)
            {
                return Ok(new { status = "fail", message = e.Message, responseObject = "" });
            }
            return Ok(new { status = "ok", message = "", responseObject = "" });
        }

        [Authorize("Admin")]
        [HttpPost("DeleteFeedback")]
        public IActionResult Delete(IdRequest idRequest)
        {
            _feedbackService.DeleteFeedback(idRequest.Id);
            return Ok(new { status = "ok", message = "", responseObject = "" });
        }
    }
}
