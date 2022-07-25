using allu_decor_be.Authorization;
using allu_decor_be.Helpers;
using allu_decor_be.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;

namespace allu_decor_be.Services
{
    public interface IContactinfoService
    {
        IEnumerable<Contactinfo> GetAll();
        Contactinfo GetById(string id);
        void CreateContactInfo(Contactinfo contact);
        void UpdateContactInfo(Contactinfo contact);
        void DeleteContactInfo(string id);
    }
    public class ContactinfoService : IContactinfoService
    {
        private DataContext _context;
        private IJwtUtils _jwtUtils;
        private readonly AppSettings _appSettings;

        public ContactinfoService(
           DataContext context,
           IJwtUtils jwtUtils,
           IOptions<AppSettings> appSettings)
        {
            _context = context;
            _jwtUtils = jwtUtils;
            _appSettings = appSettings.Value;
        }
        public void CreateContactInfo(Contactinfo contact)
        {
            if (_context.Contactinfos.Any(c => c.Email == contact.Email))
            {
                throw new AppException("ContactInfo with email address: " + contact.Email + "already exists");
            }
            contact.Id = Guid.NewGuid().ToString();
            _context.Contactinfos.Add(contact);
            _context.SaveChanges();
        }

        public void DeleteContactInfo(string id)
        {
            Contactinfo contact = getContactInfoById(id);
            _context.Contactinfos.Remove(contact);
            _context.SaveChanges();
        }

        public IEnumerable<Contactinfo> GetAll()
        {
            return _context.Contactinfos;
        }

        public Contactinfo GetById(string id)
        {
            return getContactInfoById(id);
        }

        public void UpdateContactInfo(Contactinfo contact)
        {
            Contactinfo foundContact = getContactInfoById(contact.Id);
            foundContact.Address = contact.Address;
            foundContact.Ward = contact.Ward;
            foundContact.City = contact.City;
            foundContact.Phone = contact.Phone;
            foundContact.Email = contact.Email;
            _context.Contactinfos.Update(foundContact);
            _context.SaveChanges();
        }
        private Contactinfo getContactInfoById(string id)
        {
            Contactinfo contact = _context.Contactinfos.Find(id);
            if (contact == null)
            {
                throw new KeyNotFoundException("Cannot find ContactInfo with id: " + id);
            }
            return contact;
        }
    }
}
