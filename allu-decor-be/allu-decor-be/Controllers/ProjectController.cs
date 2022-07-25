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
    public class ProjectController : ControllerBase
    {
        private IProjectService _projectService;

        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var project = _projectService.GetAll();
            return Ok(new { status = "ok", message = "", responseObject = project });

        }

        [HttpGet("id")]
        public IActionResult GetById(string id)
        {
            Project project;
            try
            {
                project = _projectService.GetProjectById(id);
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
            _projectService.CreateProject(project);
            return Ok(new { status = "ok", message = "", responseObject = "" });
        }

        [HttpPost("UpdateProject")]
        public IActionResult UpdateProject(Project project)
        {
            try
            {
                _projectService.UpdateProject(project);
            }
            catch (KeyNotFoundException e)
            {
                return Ok(new { status = "fail", message = e.Message, responseObject = "" });
            }
            return Ok(new { status = "ok", message = "", responseObject = "" });

        }

        [HttpPost("DeleteProject")]
        public IActionResult DeleteProject(IdRequest idRequest)
        {
            _projectService.DeleteProject(idRequest.Id);
            return Ok(new { status = "ok", message = "", responseObject = "" });
        }
    }
}
