using Microsoft.EntityFrameworkCore;
using YuNilarThinn.MiniPOS.AppDbContextModels;
using YuNilarThinn.MiniPOS.Models;
using YuNilarThinn.MiniPOS.Repository.Interfaces;

namespace YuNilarThinn.MiniPOS.Repository.Implementation
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _db;
        public ProductRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<ProducatListModel> GetAllProductList()
        {
            var products = await _db.Products.AsNoTracking()
                .Select(p => new ProductModel
                {
                    ProductId = p.ProductId,
                    Name = p.Name,
                    Price = p.Price,
                }).ToListAsync();

            var result = new ProducatListModel
            {
                Products = products
            };
            return result;
        }

        public async Task<int> UpdateProduct(ProductModel model)
        {
            var product = await _db.Products.FirstOrDefaultAsync(x => x.ProductId == model.ProductId);

            if (product == null)
                return 0; 

            product.Name = model.Name;
            product.Price = model.Price;

            return await _db.SaveChangesAsync();
        }

        public async Task<int> CreateProduct(ProductModel model)
        {
            var create = new Product
            {
                Name = model.Name,
                Price = model.Price,
            };

            await _db.Products.AddAsync(create);
            return await _db.SaveChangesAsync();
        }

        public async Task<ProductModel?> GetProductById(int id)
        {
            return await _db.Products
                .Where(p => p.ProductId == id)
                .Select(p => new ProductModel
                {
                    ProductId = p.ProductId,
                    Name = p.Name,
                    Price = p.Price
                })
                .FirstOrDefaultAsync();
        }
    }
}
