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
    public class InvoiceController : ControllerBase
    {
        private IInvoiceService _invoiceService;

        public InvoiceController(IInvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var invoices = _invoiceService.GetAll();
            return Ok(new { status = "ok", message = "", responseObject = invoices });
        }

        [HttpPost("ByUserId")]
        public IActionResult GetAllByUserId(IdRequest idRequest)
        {
            var invoices = _invoiceService.GetAllByUserId(idRequest.Id);
            return Ok(new { status = "ok", message = "", responseObject = invoices });
        }

        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            Invoice invoice;
            try
            {
                invoice = _invoiceService.GetById(id);
            }
            catch (KeyNotFoundException e)
            {
                return Ok(new { status = "fail", message = e.Message, data = "" });
            }

            return Ok(new { status = "ok", message = "", responseObject = invoice });
        }

        [HttpPost("CreateInvoice")]
        public IActionResult Create(Invoice invoice)
        {
            IdRequest invoiceId = _invoiceService.CreateInvoice(invoice);
            return Ok(new { status = "ok", message = "", responseObject = invoiceId });
        }

        [HttpPost("UpdateInvoice")]
        public IActionResult Update(Invoice invoice)
        {
            try
            {
                _invoiceService.UpdateInvoice(invoice);
            }
            catch (KeyNotFoundException e)
            {
                return Ok(new { status = "fail", message = e.Message, responseObject = "" });
            }
            return Ok(new { status = "ok", message = "", responseObject = "" });
        }

        [HttpPost("DeleteInvoice")]
        public IActionResult Delete(IdRequest idRequest)
        {
            _invoiceService.DeleteInvoice(idRequest.Id);
            return Ok(new { status = "ok", message = "", responseObject = "" });
        }
    }
}
