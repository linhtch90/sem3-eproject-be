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
    public class UserController : ControllerBase
    {
        private IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("[action]")]
        public IActionResult Authenticate(AuthenticateRequest model)
        {
            var response = _userService.Authenticate(model);
            return Ok(response);
        }

        [Authorize("Admin")]
        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _userService.GetAll();
            return Ok(new { status = "ok", message = "", responseObject = users });
        }   

        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            // only admins can access other user records
            //var currentUser = (User)HttpContext.Items["User"];
            //if (id != currentUser.Id && currentUser.Role != "Admin")
            //    return Unauthorized(new { message = "Unauthorized" });

            var user = _userService.GetById(id);
            return Ok(new { status = "ok", message = "", responseObject = user });
        }

        [AllowAnonymous]
        [HttpPost("CreateUser")]
        public IActionResult Create(User user)
        {
            _userService.CreateUser(user);
            return Ok(new { message = "User created"});
        }

        [HttpPost("UpdateUserWithoutPassword")]
        public IActionResult UpdateUserWhioutPassword(User user)
        {
            try
            {
                _userService.UpdateUserWithoutPassword(user);
            }
            catch (KeyNotFoundException e)
            {
                return Ok(new { status = "fail", message = e.Message, responseObject = "" });
            }
            return Ok(new { status = "ok", message = "", responseObject = "" });
        }

        [HttpPost("UpdateUser")]
        public IActionResult UpdateUser(User user)
        {
            try
            {
                _userService.UpdateUser(user);
            }
            catch (KeyNotFoundException e)
            {
                return Ok(new { status = "fail", message = e.Message, responseObject = "" });
            }
            return Ok(new { status = "ok", message = "", responseObject = "" });
        }

        [HttpPost("ChangePassword")]
        public IActionResult ChangePassword(User user)
        {
            try
            {
                _userService.ChangePassword(user);
            }
            catch (KeyNotFoundException e)
            {
                return Ok(new { status = "fail", message = e.Message, responseObject = "" });
            }
            return Ok(new { status = "ok", message = "", responseObject = "" });
        }

        [HttpPost("DeleteUser")]
        public IActionResult DeleteUser(IdRequest idRequest)
        {
            _userService.DeleteUser(idRequest.Id);
            return Ok(new { status = "ok", message = "", responseObject = "" });
        }


    }
}
