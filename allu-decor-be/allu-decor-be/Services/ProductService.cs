using allu_decor_be.Authorization;
using allu_decor_be.Helpers;
using allu_decor_be.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;

namespace allu_decor_be.Services
{
    public interface IProductService
    {
        IEnumerable<Product> GetAll();
        IEnumerable<Product> GetAllByName(string name);
        IEnumerable<Product> GetAllByDomainId(string id);
        IEnumerable<Product> GetAllByServiceId(string id);
        Product GetById(string id);
        void CreateProduct(Product product);
        void UpdateProduct(Product product);
        void DeleteProduct(string id);
    }
    public class ProductService : IProductService
    {
        private DataContext _context;
        private IJwtUtils _jwtUtils;
        private readonly AppSettings _appSettings;

        public ProductService(
            DataContext context,
            IJwtUtils jwtUtils,
            IOptions<AppSettings> appSettings)
        {
            _context = context;
            _jwtUtils = jwtUtils;
            _appSettings = appSettings.Value;
        }
        public void CreateProduct(Product product)
        {
            product.Id = Guid.NewGuid().ToString();
            _context.Products.Add(product);
            _context.SaveChanges();
        }

        public void DeleteProduct(string id)
        {
            Product product = getProductById(id);
            _context.Products.Remove(product);
            _context.SaveChanges();
        }

        public IEnumerable<Product> GetAll()
        {
            return _context.Products;
        }

        public IEnumerable<Product> GetAllByName(string name)
        {
            return _context.Products.Where(p => EF.Functions.ILike(p.Name, $"%{name}%")).ToList();
        }

        public IEnumerable<Product> GetAllByDomainId(string id)
        {
            return _context.Products.Where(p => p.Domainid == id).ToList();
        }

        public IEnumerable<Product> GetAllByServiceId(string id)
        {
            return _context.Products.Where(p => p.Serviceid == id).ToList();
        }

        public Product GetById(string id)
        {
            return getProductById(id);
        }

        public void UpdateProduct(Product product)
        {
            Product foundProduct = getProductById(product.Id);
            foundProduct.Name = product.Name;            
            foundProduct.Price = product.Price;
            foundProduct.Description = product.Description;
            foundProduct.Domainid = product.Domainid;
            foundProduct.Serviceid = product.Serviceid;
            if (product.Image != null)
            {
                foundProduct.Image = product.Image;
            }

            _context.Products.Update(foundProduct);
            _context.SaveChanges();
        }

        private Product getProductById(string id)
        {
            Product product = _context.Products.Find(id);
            if (product == null)
            {
                throw new KeyNotFoundException("Cannot find product with id: " + id);
            }
            return product;
        }
    }
}
