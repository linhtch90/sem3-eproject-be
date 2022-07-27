using allu_decor_be.Authorization;
using allu_decor_be.Helpers;
using allu_decor_be.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;

namespace allu_decor_be.Services
{
    public interface IInvoiceService
    {
        IEnumerable<Invoice> GetAll();
        Invoice GetById(string id);
        void CreateInvoice(Invoice invoice);
        void UpdateInvoice(Invoice invoice);
        void DeleteInvoice(string id);
    }
    public class InvoiceService : IInvoiceService
    {
        private DataContext _context;
        private IJwtUtils _jwtUtils;
        private readonly AppSettings _appSettings;

        public InvoiceService(
            DataContext context,
            IJwtUtils jwtUtils,
            IOptions<AppSettings> appSettings)
        {
            _context = context;
            _jwtUtils = jwtUtils;
            _appSettings = appSettings.Value;
        }
        public void CreateInvoice(Invoice invoice)
        {
            invoice.Id = Guid.NewGuid().ToString();
            _context.Invoices.Add(invoice);
            _context.SaveChanges();
        }

        public void DeleteInvoice(string id)
        {
            Invoice invoice = getInvoiceById(id);
            _context.Invoices.Remove(invoice);
            _context.SaveChanges();
        }

        public IEnumerable<Invoice> GetAll()
        {
            return _context.Invoices;
        }

        public Invoice GetById(string id)
        {
            return getInvoiceById(id);
        }

        public void UpdateInvoice(Invoice invoice)
        {
            Invoice foundInvoice = getInvoiceById(invoice.Id);
            foundInvoice.Createat = invoice.Createat;
            foundInvoice.Totalprice = invoice.Totalprice;
            foundInvoice.Status = invoice.Status;
            
            _context.Invoices.Update(foundInvoice);
            _context.SaveChanges();
        }

        private Invoice getInvoiceById(string id)
        {
            Invoice invoice = _context.Invoices.Find(id);
            if (invoice == null)
            {
                throw new KeyNotFoundException("Cannot find invoice with id: " + id);
            }
            return invoice;
        }
    }
}
