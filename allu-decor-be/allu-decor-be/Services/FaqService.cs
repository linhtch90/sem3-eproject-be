using allu_decor_be.Authorization;
using allu_decor_be.Helpers;
using allu_decor_be.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;

namespace allu_decor_be.Services
{
    public interface IFaqService
    {
        IEnumerable<Faq> GetAll();
        Faq GetById(string id);
        void CreateFaq(Faq faq);
        void UpdateFaq(Faq faq);    
        void DeleteFaq(string id);
    }
    public class FaqService : IFaqService
    {
        private DataContext _context;
        private IJwtUtils _jwtUtils;
        private readonly AppSettings _appSettings;

        public FaqService(   DataContext context,
           IJwtUtils jwtUtils,
           IOptions<AppSettings> appSettings)
        {
            _context = context;
            _jwtUtils = jwtUtils;
            _appSettings = appSettings.Value;
        }


        public void CreateFaq(Faq faq)
        {
            faq.Id = Guid.NewGuid().ToString();
            _context.Faqs.Add(faq);
            _context.SaveChanges();
        }

        public void DeleteFaq(string id)
        {
            Faq faq = getFaqById(id);
            _context.Faqs.Remove(faq);
            _context.SaveChanges();
        }

        public IEnumerable<Faq> GetAll()
        {
            return _context.Faqs;
        }

        public Faq GetById(string id)
        {
            return getFaqById(id);
        }

        public void UpdateFaq(Faq faq)
        {
            Faq foundFaq = getFaqById(faq.Id);
            foundFaq.Answer = faq.Answer;
            foundFaq.Question = faq.Question;
            _context.Faqs.Update(foundFaq);
            _context.SaveChanges();
        }

        private Faq getFaqById(string id)
        {
            Faq faq = _context.Faqs.Find(id);
            if (faq == null)
            {
                throw new KeyNotFoundException("Cannot find faq with id: " + id);
            }
            return faq;
        }

    }
}
