using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using YuNilarThinn.MiniPOS.AppDbContextModels;
using YuNilarThinn.MiniPOS.Models;
using YuNilarThinn.MiniPOS.Repository.Interfaces;

namespace YuNilarThinn.MiniPOS.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _repo;
        public ProductController(IProductRepository repo)
        {
            _repo = repo;
        }

        public async Task<IActionResult> Index()
        {
            var product = await _repo.GetAllProductList();
            return View(product);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _repo.GetProductById(id.Value); 

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpPost]

        public async Task<IActionResult> Edit(ProductModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            await _repo.UpdateProduct(model);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductModel model)
        {
            var result = await _repo.CreateProduct(model);
            return RedirectToAction(nameof(Index));
        }
    }
}
