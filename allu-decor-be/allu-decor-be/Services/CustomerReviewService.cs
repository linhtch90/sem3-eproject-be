using allu_decor_be.Authorization;
using allu_decor_be.Helpers;
using allu_decor_be.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;

namespace allu_decor_be.Services
{
    public interface ICustomerReviewService
    {
        IEnumerable<Customerreview> GetAll();
        Customerreview GetById(string id);
        void CreateCustomerreview(Customerreview customerreview);
        void UpdateCustomerreview(Customerreview customerreview);
        void DeleteCustomerreview(string id);
    }
    public class CustomerReviewService : ICustomerReviewService
    {
        private DataContext _context;
        private IJwtUtils _jwtUtils;
        private readonly AppSettings _appSettings;

        public CustomerReviewService(
            DataContext context,
            IJwtUtils jwtUtils,
            IOptions<AppSettings> appSettings)
        {
            _context = context;
            _jwtUtils = jwtUtils;
            _appSettings = appSettings.Value;
        }
        public void CreateCustomerreview(Customerreview customerreview)
        {
            customerreview.Id = Guid.NewGuid().ToString();
            _context.Customerreviews.Add(customerreview);
            _context.SaveChanges();
        }
        public void DeleteCustomerreview(string id)
        {
            Customerreview customerreview = getCustomerreviewById(id);
            _context.Customerreviews.Remove(customerreview);
            _context.SaveChanges();
        }
        public IEnumerable<Customerreview> GetAll()
        {
            return _context.Customerreviews;
        }
        public Customerreview GetById(string id)
        {
            return getCustomerreviewById(id);
        }
        public void UpdateCustomerreview(Customerreview customerreview)
        {
            Customerreview foundCustomerreview = getCustomerreviewById(customerreview.Id);
            foundCustomerreview.Firstname = customerreview.Firstname;
            foundCustomerreview.Lastname = customerreview.Lastname;
            foundCustomerreview.Company = customerreview.Company;
            foundCustomerreview.Content = customerreview.Content;
            _context.Customerreviews.Update(foundCustomerreview);
            _context.SaveChanges();
        }
        private Customerreview getCustomerreviewById(string id)
        {
            Customerreview customerreview = _context.Customerreviews.Find(id);
            if (customerreview == null)
            {
                throw new KeyNotFoundException("Cannot find customer review with id: " + id);
            }
            return customerreview;
        }
    }

}
