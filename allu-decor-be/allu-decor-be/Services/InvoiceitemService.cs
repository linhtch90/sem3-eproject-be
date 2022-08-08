using allu_decor_be.Authorization;
using allu_decor_be.Helpers;
using allu_decor_be.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;

namespace allu_decor_be.Services
{
    public interface IInvoiceitemService
    {
        IEnumerable<Invoiceitem> GetAll();
        IEnumerable<Invoiceitem> GetAllByInvoiceId(string id);
        Invoiceitem GetById(string id);
        void CreateInvoiceitem(Invoiceitem invoiceitem);
        void UpdateInvoiceitem(Invoiceitem invoiceitem);
        void DeleteInvoiceitem(string id);
    }
    public class InvoiceitemService : IInvoiceitemService
    {
        private DataContext _context;
        private IJwtUtils _jwtUtils;
        private readonly AppSettings _appSettings;

        public InvoiceitemService(
            DataContext context,
            IJwtUtils jwtUtils,
            IOptions<AppSettings> appSettings)
        {
            _context = context;
            _jwtUtils = jwtUtils;
            _appSettings = appSettings.Value;
        }
        public void CreateInvoiceitem(Invoiceitem invoiceitem)
        {
            invoiceitem.Id = Guid.NewGuid().ToString();
            _context.Invoiceitems.Add(invoiceitem);
            _context.SaveChanges();
        }

        public void DeleteInvoiceitem(string id)
        {
            Invoiceitem invoiceitem = getInvoiceitemById(id);
            _context.Invoiceitems.Remove(invoiceitem);
            _context.SaveChanges();
        }

        public IEnumerable<Invoiceitem> GetAll()
        {
            return _context.Invoiceitems;
        }

        public IEnumerable<Invoiceitem> GetAllByInvoiceId(string id)
        {
            var invoiceitems = _context.Invoiceitems.Where(item => item.Invoiceid == id).ToList();
            foreach (var invoiceitem in invoiceitems)
            {
                invoiceitem.Invoice = null;
                invoiceitem.Product = null;
            }
            return invoiceitems;
        }

        public Invoiceitem GetById(string id)
        {
            return getInvoiceitemById(id);
        }

        public void UpdateInvoiceitem(Invoiceitem invoiceitem)
        {
            Invoiceitem foundInvoiceitem = getInvoiceitemById(invoiceitem.Id);
            foundInvoiceitem.Quantity = invoiceitem.Quantity;
            foundInvoiceitem.Totalprice = invoiceitem.Totalprice;
            _context.Invoiceitems.Update(foundInvoiceitem);
            _context.SaveChanges();
        }
        private Invoiceitem getInvoiceitemById(string id)
        {
            Invoiceitem invoiceitem = _context.Invoiceitems.Find(id);
            if (invoiceitem == null)
            {
                throw new KeyNotFoundException("Cannot find invoiceitem with id: " + id);
            }
            return invoiceitem;
        }
    }
}
