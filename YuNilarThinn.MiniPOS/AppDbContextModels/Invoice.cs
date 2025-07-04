using System;
using System.Collections.Generic;

namespace YuNilarThinn.MiniPOS.AppDbContextModels;

public partial class Invoice
{
    public int SalesInvoiceId { get; set; }

    public DateOnly SalesDate { get; set; }

    public int CustomerId { get; set; }

    public string? Remark { get; set; }

    public decimal TotalAmount { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual ICollection<InvoiceItem> InvoiceItems { get; set; } = new List<InvoiceItem>();
}
