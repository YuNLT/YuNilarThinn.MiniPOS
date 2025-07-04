namespace YuNilarThinn.MiniPOS.Models
{
    public class SaleModels
    {
        public string CustomerCode { get; set; }
        public ProducatListModel Products { get; set; }
        public CustomerListModel Customers { get; set; }
        public List<CartItemModel> CartItems { get; set; }= new (); 
        public decimal TotalAmount => CartItems.Sum(x => x.Amount);
    }
    public class CartItemModel
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Qty { get; set; }
        public decimal Amount
        {
            get; set;
        }
    }
}
