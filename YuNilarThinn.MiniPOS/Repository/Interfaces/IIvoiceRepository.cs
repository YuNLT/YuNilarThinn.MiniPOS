using YuNilarThinn.MiniPOS.Models;

namespace YuNilarThinn.MiniPOS.Repository.Interfaces
{
    public interface IIvoiceRepository
    {
        Task<List<InvoiceModel>> GetAllInvoice();
        Task<int> CreateInvice(InvoiceModel model);
        Task<int> UpdateInvoice(InvoiceModel model);
    }
}
