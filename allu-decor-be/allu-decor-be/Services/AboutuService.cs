using allu_decor_be.Authorization;
using allu_decor_be.Helpers;
using allu_decor_be.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;

namespace allu_decor_be.Services
{
    public interface IAboutuService
    {
        IEnumerable<Aboutu> GetAll();
        Aboutu GetById(string id);
        void CreateAboutu(Aboutu aboutu);
        void UpdateAboutu(Aboutu aboutu);
        void DeleteAboutu(string id);
    }
    public class AboutuService : IAboutuService
    {
        private DataContext _context;
        private IJwtUtils _jwtUtils;
        private readonly AppSettings _appSettings;

        public AboutuService(
            DataContext context,
            IJwtUtils jwtUtils,
            IOptions<AppSettings> appSettings)
        {
            _context = context;
            _jwtUtils = jwtUtils;
            _appSettings = appSettings.Value;
        }

        public IEnumerable<Aboutu> GetAll()
        {
            return _context.Aboutus;
        }

        public Aboutu GetById(string id)
        {
            return getAboutuById(id);
        }

        public void CreateAboutu(Aboutu aboutu)
        {
            aboutu.Id = Guid.NewGuid().ToString();
            _context.Aboutus.Add(aboutu);
            _context.SaveChanges();
        }

        public void UpdateAboutu(Aboutu aboutu)
        {
            Aboutu foundAboutu = getAboutuById(aboutu.Id);
            foundAboutu.Content = aboutu.Content;
            _context.Aboutus.Update(foundAboutu);
            _context.SaveChanges();
        }

        public void DeleteAboutu(string id)
        {
            Aboutu aboutu = getAboutuById(id);
            _context.Aboutus.Remove(aboutu);
            _context.SaveChanges();
        }

        private Aboutu getAboutuById(string id)
        {
            Aboutu aboutu = _context.Aboutus.Find(id);
            if (aboutu == null)
            {
                throw new KeyNotFoundException("Cannot find aboutu id: " + id);
            }
            return aboutu;
        }
    }
}
