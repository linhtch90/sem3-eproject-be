using allu_decor_be.Models;
using allu_decor_be.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace allu_decor_be.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private IProjectService _project;

        public ProjectController(IProjectService project)
        {
            _project = project;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var project = _project.GetAll();
            return Ok(new { status = "ok", message = "", responseObject = project });

        }

        [HttpGet("id")]
        public IActionResult GetById(string id)
        {
            Project project;
            try
            {
                project = _project.GetProjectById(id);
            }
            catch (KeyNotFoundException e)
            {
                return Ok(new { status = "fail", message = e.Message, data = "" });
            }

            return Ok(new { status = "ok", message = "", responseObject = project });
        }

        [HttpPost("CreateProject")]
        public IActionResult Create(Project project)
        {
            _project.CreateProject(project);
            return Ok(new { status = "ok", message = "", responseObject = "" });
        }

        [HttpPut("UpdateProject")]
        public IActionResult UpdateProject(Project project)
        {
            try
            {
                _project.UpdateProject(project);
            }
            catch (KeyNotFoundException e)
            {
                return Ok(new { status = "fail", message = e.Message, responseObject = "" });
            }
            return Ok(new { status = "ok", message = "", responseObject = "" });

        }

        [HttpPost ("DeleteProject")]
        public IActionResult DeleteProject(IdRequest idRequest)
        {
            _project.DeleteProject(idRequest.Id);
            return Ok(new { status = "ok", message = "", responseObject = "" });
        }
    }
}
