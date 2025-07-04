namespace YuNilarThinn.MiniPOS.Models
{
    public class CustomerModel
    {
        public int CustomerId { get; set; }

        public string CustomerCode { get; set; } = null!;

        public string CustomerName { get; set; } = null!;

        public string? PhoneNo { get; set; }

        public string? Address { get; set; }
    }

    public class CustomerListModel
    {
        public List<CustomerModel> Customers { get; set; }
    }
}
