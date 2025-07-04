using Microsoft.AspNetCore.Mvc;
using YuNilarThinn.MiniPOS.Models;
using YuNilarThinn.MiniPOS.Repository.Implementation;
using YuNilarThinn.MiniPOS.Repository.Interfaces;

namespace YuNilarThinn.MiniPOS.Controllers
{
    public class InvoiceController : Controller
    {
        private readonly IIvoiceRepository _invoiceRepo;

        public InvoiceController(IIvoiceRepository invoiceRepo)
        {
            _invoiceRepo = invoiceRepo;
        }

        public async Task<IActionResult> Index()
        {
            var invoices = await _invoiceRepo.GetAllInvoice();
            return View(invoices);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var invoices = await _invoiceRepo.GetAllInvoice();
            var invoice = invoices.FirstOrDefault(x => x.SalesInvoiceId == id);

            if (invoice == null)
                return NotFound();

            return View(invoice);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(InvoiceModel model)
        {
            await _invoiceRepo.UpdateInvoice(model);
            return RedirectToAction(nameof(Index));
        }
    }
}
