using allu_decor_be.Authorization;
using allu_decor_be.Helpers;
using allu_decor_be.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;

namespace allu_decor_be.Services
{
    public interface IDomainService
    {
        IEnumerable<Domain> GetAll();
        Domain GetById(string id);
        void CreateDomain(Domain domain);
        void UpdateDomain(Domain domain);
        void DeleteDomain(string id);
    }
    public class DomainService : IDomainService
    {
        private DataContext _context;
        private IJwtUtils _jwtUtils;
        private readonly AppSettings _appSettings;

        public DomainService(
            DataContext context,
            IJwtUtils jwtUtils,
            IOptions<AppSettings> appSettings)
        {
            _context = context;
            _jwtUtils = jwtUtils;
            _appSettings = appSettings.Value;
        }
        public void CreateDomain(Domain domain)
        {
            domain.Id = Guid.NewGuid().ToString();
            _context.Domains.Add(domain);
            _context.SaveChanges();
        }

        public void DeleteDomain(string id)
        {
            Domain domain = getDomainById(id);
            _context.Domains.Remove(domain);
            _context.SaveChanges();
        }

        public IEnumerable<Domain> GetAll()
        {
            return _context.Domains;
        }

        public Domain GetById(string id)
        {
            return getDomainById(id);
        }

        public void UpdateDomain(Domain domain)
        {
            Domain foundDomain = getDomainById(domain.Id);
            foundDomain.Name = domain.Name;
            _context.Domains.Update(foundDomain);
            _context.SaveChanges();
        }
        
        private Domain getDomainById(string id)
        {
            Domain domain = _context.Domains.Find(id);
            if (domain == null)
            {
                throw new KeyNotFoundException("Cannot find domain with id: " + id);
            }
            return domain;
        }
    }
}
