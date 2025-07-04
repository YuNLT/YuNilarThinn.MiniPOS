using YuNilarThinn.MiniPOS.Models;

namespace YuNilarThinn.MiniPOS.Repository.Interfaces
{
    public interface ICustomerRepository
    {
        Task<CustomerListModel> GetAllCustomer();
        Task<int> UpdateCustomer(CustomerModel model);
        Task<int> CreateCustomer(CustomerModel model);
        Task<CustomerModel> GetByCodeAsync(string code);

        Task<CustomerModel> GetCustomerById(int id);
    }
}
