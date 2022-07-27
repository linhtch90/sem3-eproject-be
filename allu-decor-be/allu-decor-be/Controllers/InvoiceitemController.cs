using allu_decor_be.Authorization;
using allu_decor_be.Models;
using allu_decor_be.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace allu_decor_be.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceitemController : ControllerBase
    {
        private IInvoiceitemService _invoiceitemService;

        public InvoiceitemController(IInvoiceitemService invoiceitemService)
        {
            _invoiceitemService = invoiceitemService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var invoiceitems = _invoiceitemService.GetAll();
            return Ok(new { status = "ok", message = "", responseObject = invoiceitems });
        }

        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            Invoiceitem invoiceitem;
            try
            {
                invoiceitem = _invoiceitemService.GetById(id);
            }
            catch (KeyNotFoundException e)
            {
                return Ok(new { status = "fail", message = e.Message, data = "" });
            }

            return Ok(new { status = "ok", message = "", responseObject = invoiceitem });
        }

        [HttpPost("CreateInvoiceitem")]
        public IActionResult Create(Invoiceitem invoiceitem)
        {
            _invoiceitemService.CreateInvoiceitem(invoiceitem);
            return Ok(new { status = "ok", message = "", responseObject = "" });
        }

        [HttpPost("UpdateInvoiceitem")]
        public IActionResult Update(Invoiceitem invoiceitem)
        {
            try
            {
                _invoiceitemService.UpdateInvoiceitem(invoiceitem);
            }
            catch (KeyNotFoundException e)
            {
                return Ok(new { status = "fail", message = e.Message, responseObject = "" });
            }
            return Ok(new { status = "ok", message = "", responseObject = "" });
        }

        [HttpPost("DeleteInvoiceitem")]
        public IActionResult Delete(IdRequest idRequest)
        {
            _invoiceitemService.DeleteInvoiceitem(idRequest.Id);
            return Ok(new { status = "ok", message = "", responseObject = "" });
        }
    }
}
