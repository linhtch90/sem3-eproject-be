using allu_decor_be.Authorization;
using allu_decor_be.Helpers;
using allu_decor_be.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;

namespace allu_decor_be.Services
{
    public interface IDomainserviceService
    {
        IEnumerable<Domainservice> GetAll();
        Domainservice GetById(string id);
        string GetIdFromDomainIdAndServiceId(string domainId, string serviceId);
        void CreateDomainservice(Domainservice domainservice);
        void UpdateDomainservice(Domainservice domainservice);
        void DeleteDomainservice(string id);

    }
    public class DomainserviceService : IDomainserviceService
    {
        private DataContext _context;
        private IJwtUtils _jwtUtils;
        private readonly AppSettings _appSettings;

        public DomainserviceService(
            DataContext context,
            IJwtUtils jwtUtils,
            IOptions<AppSettings> appSettings)
        {
            _context = context;
            _jwtUtils = jwtUtils;
            _appSettings = appSettings.Value;
        }
        public void CreateDomainservice(Domainservice domainservice)
        {
            domainservice.Id = Guid.NewGuid().ToString();
            _context.Domainservices.Add(domainservice);
            _context.SaveChanges();
        }

        public void DeleteDomainservice(string id)
        {
            Domainservice domainservice = getDomainserviceById(id);
            _context.Domainservices.Remove(domainservice);
            _context.SaveChanges();
        }

        public IEnumerable<Domainservice> GetAll()
        {
            return _context.Domainservices;
        }

        public Domainservice GetById(string id)
        {
            return getDomainserviceById(id);
        }

        public string GetIdFromDomainIdAndServiceId(string domainId, string serviceId)
        {
            return GetAll().ToList().Where(item => item.Domainid == domainId && item.Serviceid == serviceId).First().Id;
        }

        public void UpdateDomainservice(Domainservice domainservice)
        {
            Domainservice foundDomainservice = getDomainserviceById(domainservice.Id);
            foundDomainservice.Domainid = domainservice.Domainid;
            foundDomainservice.Serviceid = domainservice.Serviceid;
            _context.Domainservices.Update(foundDomainservice);
            _context.SaveChanges();
        }

        private Domainservice getDomainserviceById(string id)
        {
            Domainservice domainservice = _context.Domainservices.Find(id);
            if (domainservice == null)
            {
                throw new KeyNotFoundException("Cannot find domainservice with id: " + id);
            }
            return domainservice;
        }
    }
}
