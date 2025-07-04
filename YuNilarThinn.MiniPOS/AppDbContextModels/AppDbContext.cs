using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace YuNilarThinn.MiniPOS.AppDbContextModels;

public partial class AppDbContext : DbContext
{

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Invoice> Invoices { get; set; }

    public virtual DbSet<InvoiceItem> InvoiceItems { get; set; }

    public virtual DbSet<Product> Products { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK__Customer__A4AE64B84B25209E");

            entity.ToTable("Customer");

            entity.HasIndex(e => e.CustomerCode, "UQ__Customer__0667852111589395").IsUnique();

            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.Address).HasMaxLength(200);
            entity.Property(e => e.CustomerCode).HasMaxLength(10);
            entity.Property(e => e.CustomerName).HasMaxLength(100);
            entity.Property(e => e.PhoneNo).HasMaxLength(20);
        });

        modelBuilder.Entity<Invoice>(entity =>
        {
            entity.HasKey(e => e.SalesInvoiceId).HasName("PK__Invoice__BA05C2FA354DC164");

            entity.ToTable("Invoice");

            entity.Property(e => e.SalesInvoiceId).HasColumnName("SalesInvoiceID");
            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.Remark).HasMaxLength(200);
            entity.Property(e => e.TotalAmount).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Customer).WithMany(p => p.Invoices)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Invoice_Customer");
        });

        modelBuilder.Entity<InvoiceItem>(entity =>
        {
            entity.HasKey(e => e.SalesInvoiceItemId).HasName("PK__InvoiceI__BA84EC04180D3317");

            entity.ToTable("InvoiceItem");

            entity.Property(e => e.SalesInvoiceItemId).HasColumnName("SalesInvoiceItemID");
            entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.SalesInvoiceId).HasColumnName("SalesInvoiceID");

            entity.HasOne(d => d.Product).WithMany(p => p.InvoiceItems)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_InvoiceItem_Product");

            entity.HasOne(d => d.SalesInvoice).WithMany(p => p.InvoiceItems)
                .HasForeignKey(d => d.SalesInvoiceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_InvoiceItem_Invoice");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__Product__B40CC6ED1D3C98F7");

            entity.ToTable("Product");

            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
