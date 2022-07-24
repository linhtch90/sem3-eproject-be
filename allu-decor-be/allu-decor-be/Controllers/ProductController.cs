using allu_decor_be.Models;
using allu_decor_be.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace allu_decor_be.Controllers
{
    public class ProductController : ControllerBase
    {
        private IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var domains = _productService.GetAll();
            return Ok(new { status = "ok", message = "", responseObject = domains });
        }

        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            Product product;
            try
            {
                product = _productService.GetById(id);
            }
            catch (KeyNotFoundException e)
            {
                return Ok(new { status = "fail", message = e.Message, data = "" });
            }

            return Ok(new { status = "ok", message = "", responseObject = product });
        }

        [HttpPost("CreateProduct")]
        public IActionResult Create(Product product)
        {
            _productService.CreateProduct(product);
            return Ok(new { status = "ok", message = "", responseObject = "" });
        }

        [HttpPost("UpdateProduct")]
        public IActionResult Update(Product product)
        {
            try
            {
                _productService.UpdateProduct(product);
            }
            catch (KeyNotFoundException e)
            {
                return Ok(new { status = "fail", message = e.Message, responseObject = "" });
            }
            return Ok(new { status = "ok", message = "", responseObject = "" });
        }

        [HttpPost("DeleteProduct")]
        public IActionResult Delete(IdRequest idRequest)
        {
            _productService.DeleteProduct(idRequest.Id);
            return Ok(new { status = "ok", message = "", responseObject = "" });
        }
    }
}
