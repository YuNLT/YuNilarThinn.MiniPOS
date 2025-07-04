using System;
using System.Collections.Generic;

namespace YuNilarThinn.MiniPOS.AppDbContextModels;

public partial class InvoiceItem
{
    public int SalesInvoiceItemId { get; set; }

    public int SalesInvoiceId { get; set; }

    public int ProductId { get; set; }

    public int Qty { get; set; }

    public decimal Price { get; set; }

    public decimal Amount { get; set; }

    public virtual Product Product { get; set; } = null!;

    public virtual Invoice SalesInvoice { get; set; } = null!;
}
