namespace YuNilarThinn.MiniPOS.Models
{
    public class InvoiceModel
    {
        public int SalesInvoiceId { get; set; }

        public DateOnly SalesDate { get; set; }

        public int CustomerId { get; set; }

        public string? Remark { get; set; }

        public decimal TotalAmount { get; set; }

        public List<InvoiceItemModel> InvoiceItems { get; set; } = new List<InvoiceItemModel>();
    }

    public class InvoiceItemModel
    {
        public int SalesInvoiceItemId { get; set; }

        public int SalesInvoiceId { get; set; }

        public int ProductId { get; set; }

        public int Qty { get; set; }

        public decimal Price { get; set; }

        public decimal Amount { get; set; }
    }
}
