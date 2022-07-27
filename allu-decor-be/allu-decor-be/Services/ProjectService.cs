using allu_decor_be.Authorization;
using allu_decor_be.Helpers;
using allu_decor_be.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;

namespace allu_decor_be.Services
{
    public interface IProjectService
    {
        IEnumerable<Project> GetAll();
        Project GetProjectById(string id);
        void CreateProject(Project project);
        void UpdateProject(Project project);
        void DeleteProject(string id);
    }


    public class ProjectService : IProjectService
    {
        private DataContext _context;
        private IJwtUtils _jwtUtils;
        private readonly AppSettings _appSettings;

        public ProjectService(
            DataContext context,
           IJwtUtils jwtUtils,
           IOptions<AppSettings> appSettings)
        {
            _context = context;
            _jwtUtils = jwtUtils;
            _appSettings = appSettings.Value;
        }

        public void CreateProject(Project project)
        {
            project.Id = Guid.NewGuid().ToString();
            _context.Projects.Add(project);
            _context.SaveChanges();
        }

        public void DeleteProject(string id)
        {
             Project project = getProjectById(id);
            _context.Projects.Remove(project);
            _context.SaveChanges();
        }

        public IEnumerable<Project> GetAll()
        {
            return _context.Projects;
        }

        public Project GetProjectById(string id)
        {
            return getProjectById(id);

        }

        public void UpdateProject(Project project)
        {
            Project foundProject = getProjectById(project.Id);
            foundProject.Name = project.Name;
            foundProject.Status = project.Status;
            foundProject.Description = project.Description;
            foundProject.Image = project.Image;

            _context.Projects.Update(foundProject);
            _context.SaveChanges();
        }

        private Project getProjectById(string id)
        {
            Project project = _context.Projects.Find(id);
            if (project == null)
            {
                throw new KeyNotFoundException("Cannot find project with id: " + id);
            }
            return project;
        }
         
    }
}
