using allu_decor_be.Authorization;
using allu_decor_be.Helpers;
using allu_decor_be.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;

namespace allu_decor_be.Services
{
    public interface IServiceService
    {
        IEnumerable<Service> GetAll();
        Service GetById(string id);
        void CreateService(Service service);
        void UpdateService(Service service);
        void DeleteService(string id);

    }
    public class ServiceService : IServiceService
    {
        private DataContext _context;
        private IJwtUtils _jwtUtils;
        private readonly AppSettings _appSettings;

        public ServiceService(
            DataContext context,
            IJwtUtils jwtUtils,
            IOptions<AppSettings> appSettings)
        {
            _context = context;
            _jwtUtils = jwtUtils;
            _appSettings = appSettings.Value;
        }

        public void CreateService(Service service)
        {
            service.Id = Guid.NewGuid().ToString();
            _context.Services.Add(service);
            _context.SaveChanges();
        }

        public void DeleteService(string id)
        {
            Service service = getServiceById(id);
            _context.Services.Remove(service);
            _context.SaveChanges();
        }

        public IEnumerable<Service> GetAll()
        {
            return _context.Services;
        }

        public Service GetById(string id)
        {
            return getServiceById(id);
        }

        public void UpdateService(Service service)
        {
            Service foundService = getServiceById(service.Id);
            foundService.Name = service.Name;
            _context.Services.Update(foundService);
            _context.SaveChanges();
        }

        private Service getServiceById(string id)
        {
            Service service = _context.Services.Find(id);
            if (service == null)
            {
                throw new KeyNotFoundException("Cannot find service with id: " + id);
            }
            return service;
        }
    }
}
