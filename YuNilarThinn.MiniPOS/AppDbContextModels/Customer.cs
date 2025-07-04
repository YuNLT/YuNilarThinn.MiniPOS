using System;
using System.Collections.Generic;

namespace YuNilarThinn.MiniPOS.AppDbContextModels;

public partial class Customer
{
    public int CustomerId { get; set; }

    public string CustomerCode { get; set; } = null!;

    public string CustomerName { get; set; } = null!;

    public string? PhoneNo { get; set; }

    public string? Address { get; set; }

    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();
}
