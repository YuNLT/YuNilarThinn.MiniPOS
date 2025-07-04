using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using YuNilarThinn.MiniPOS.Models;
using YuNilarThinn.MiniPOS.Repository.Interfaces;

namespace YuNilarThinn.MiniPOS.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerRepository _repo;
        public CustomerController(ICustomerRepository repo)
        {
            _repo = repo;
        }
        public async Task<IActionResult> Index()
        {
            var lst = await _repo.GetAllCustomer();
            return View(lst);
        }

        public async Task<IActionResult> Edit([FromRoute] int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _repo.GetCustomerById(id.Value);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpPost]

        public async Task<IActionResult> Edit(CustomerModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            await _repo.UpdateCustomer(model);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CustomerModel model)
        {
            var result = await _repo.CreateCustomer(model);
            return RedirectToAction(nameof(Index));
        }
    }
}
