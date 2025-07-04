using Microsoft.EntityFrameworkCore;
using YuNilarThinn.MiniPOS.AppDbContextModels;
using YuNilarThinn.MiniPOS.Models;
using YuNilarThinn.MiniPOS.Repository.Interfaces;

namespace YuNilarThinn.MiniPOS.Repository.Implementation
{
    public class InvoiceRepository : IIvoiceRepository
    {
        private readonly AppDbContext _db;
        public InvoiceRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<List<InvoiceModel>> GetAllInvoice()
        {
            var invoices = await _db.Invoices
        .Include(x => x.InvoiceItems)
        .ToListAsync();

            var invoiceResponse = invoices.Select(i => new InvoiceModel
            {
                SalesInvoiceId = i.SalesInvoiceId,
                SalesDate = i.SalesDate,
                CustomerId = i.CustomerId,
                Remark = i.Remark,
                TotalAmount = i.TotalAmount,
                InvoiceItems = i.InvoiceItems.Select(item => new InvoiceItemModel
                {
                    SalesInvoiceItemId = item.SalesInvoiceItemId,
                    SalesInvoiceId = item.SalesInvoiceId,
                    ProductId = item.ProductId,
                    Qty = item.Qty,
                    Price = item.Price,
                    Amount = item.Amount
                }).ToList()
            }).ToList();

            return invoiceResponse;
        }

        public async Task<int> CreateInvice(InvoiceModel model)
        {
            var invoice = new Invoice
            {
                SalesDate = model.SalesDate,
                CustomerId = model.CustomerId,
                Remark = model.Remark,
                TotalAmount = model.TotalAmount,
                InvoiceItems = model.InvoiceItems.Select(x => new InvoiceItem
                {
                    ProductId = x.ProductId,
                    Qty = x.Qty,
                    Price = x.Price,
                    Amount = x.Amount
                }).ToList()
            };

            await _db.Invoices.AddAsync(invoice);
            return await _db.SaveChangesAsync();
        }
        public async Task<int> UpdateInvoice(InvoiceModel model)
        {
            var invoice = await _db.Invoices
                .Include(i => i.InvoiceItems)
                .FirstOrDefaultAsync(i => i.SalesInvoiceId == model.SalesInvoiceId);

            if (invoice == null) return 0;

            invoice.SalesDate = model.SalesDate;
            invoice.CustomerId = model.CustomerId;
            invoice.Remark = model.Remark;
            invoice.TotalAmount = model.TotalAmount;

            _db.InvoiceItems.RemoveRange(invoice.InvoiceItems);
            invoice.InvoiceItems = model.InvoiceItems.Select(i => new InvoiceItem
            {
                ProductId = i.ProductId,
                Qty = i.Qty,
                Price = i.Price,
                Amount = i.Amount
            }).ToList();

            return await _db.SaveChangesAsync();
        }
    }
}
