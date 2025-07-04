using System;
using System.Collections.Generic;

namespace YuNilarThinn.MiniPOS.AppDbContextModels;

public partial class Product
{
    public int ProductId { get; set; }

    public string Name { get; set; } = null!;

    public decimal Price { get; set; }

    public virtual ICollection<InvoiceItem> InvoiceItems { get; set; } = new List<InvoiceItem>();
}
