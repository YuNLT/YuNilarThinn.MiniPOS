using System.ComponentModel.DataAnnotations;

namespace YuNilarThinn.MiniPOS.Models
{
    public class ProductModel
    {
        public int ProductId { get; set; }
        [Required]
        public string Name { get; set; } = null!;

        public decimal Price { get; set; }
    }

    public class ProducatListModel
    {
        public List<ProductModel> Products { get; set; }
    }
}
