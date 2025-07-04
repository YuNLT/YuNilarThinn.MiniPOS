using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Threading.Tasks;
using YuNilarThinn.MiniPOS.AppDbContextModels;
using YuNilarThinn.MiniPOS.Models;
using YuNilarThinn.MiniPOS.Repository.Implementation;
using YuNilarThinn.MiniPOS.Repository.Interfaces;

namespace YuNilarThinn.MiniPOS.Controllers
{
    public class SaleController : Controller
    {
        private readonly IIvoiceRepository _invoicerepo;
        private readonly IProductRepository _productrepo;
        private readonly ICustomerRepository _customerrepo;
        public SaleController(IIvoiceRepository salerepo, IProductRepository productrepo, ICustomerRepository customerrepo)
        {
            _invoicerepo = salerepo;
            _productrepo = productrepo;
            _customerrepo = customerrepo;
        }
        public async Task<IActionResult> Create()
        {
            var model = new SaleModels
            {
                Customers = await _customerrepo.GetAllCustomer(),
                Products = await _productrepo.GetAllProductList(),
                CartItems = new List<CartItemModel>()
            };

            return View(model);
        }

        [HttpPost]

        public async Task<IActionResult> Create(SaleModels model)
        {
            string json = Request.Form["CartItems"];
            model.CartItems = JsonConvert.DeserializeObject<List<CartItemModel>>(json);
            var customer = await _customerrepo.GetByCodeAsync(model.CustomerCode);
            if (customer == null)
            {
                ModelState.AddModelError("", "Invalid customer selected.");
                model.Customers = await _customerrepo.GetAllCustomer();
                model.Products = await _productrepo.GetAllProductList();
                return View(model);
            }

            var invoiceModel = new InvoiceModel
            {
                SalesDate = DateOnly.FromDateTime(DateTime.Now),
                CustomerId = customer.CustomerId,
                Remark = "", 
                TotalAmount = model.CartItems.Sum(x => x.Amount),
                InvoiceItems = model.CartItems.Select(x => new InvoiceItemModel
                {
                    ProductId = x.ProductID,
                    Qty = x.Qty,
                    Price = x.Price,
                    Amount = x.Amount
                }).ToList()
            };

            await _invoicerepo.CreateInvice(invoiceModel);
            return RedirectToAction("Index", "Invoice");
        }
    }
}
