using YuNilarThinn.MiniPOS.Models;

namespace YuNilarThinn.MiniPOS.Repository.Interfaces
{
    public interface IProductRepository
    {
        Task<ProducatListModel> GetAllProductList();
        Task<int> UpdateProduct(ProductModel model);
        Task<int> CreateProduct(ProductModel model);
        Task<ProductModel?> GetProductById(int id);
    }
}
