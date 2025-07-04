using Microsoft.EntityFrameworkCore;
using YuNilarThinn.MiniPOS.AppDbContextModels;
using YuNilarThinn.MiniPOS.Models;
using YuNilarThinn.MiniPOS.Repository.Interfaces;

namespace YuNilarThinn.MiniPOS.Repository.Implementation
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly AppDbContext _db;
        private readonly DevCode _customercode;
        public CustomerRepository(AppDbContext db, DevCode customercode)
        {
            _db = db;
            _customercode = customercode;
        }

        public async Task<CustomerListModel> GetAllCustomer()
        {
            var customers = await _db.Customers.AsNoTracking()
                .Select(c => new CustomerModel
                {
                   CustomerId = c.CustomerId,
                   CustomerCode = c.CustomerCode,
                   CustomerName = c.CustomerName,
                   PhoneNo = c.PhoneNo,
                   Address = c.Address,
                }).ToListAsync();

            var result = new CustomerListModel
            {
                Customers = customers
            };
            return result;
        }

        public async Task<int> UpdateCustomer(CustomerModel model)
        {
            var customer = await _db.Customers.FirstOrDefaultAsync(x => x.CustomerId == model.CustomerId);

            if (customer == null)
                return 0; 
            customer.CustomerCode = model.CustomerCode;
            customer.CustomerName = model.CustomerName;
            customer.Address = model.Address;
            customer.PhoneNo = model.PhoneNo;

            var result = await _db.SaveChangesAsync();
            return result;
        }

        public async Task<int> CreateCustomer(CustomerModel model)
        {
            var create = new Customer
            {
                CustomerCode = await _customercode.GetCustomerCode(),
                CustomerName = model.CustomerName,
                Address = model.Address,
                PhoneNo = model.PhoneNo,
            };

            await _db.Customers.AddAsync(create);
            return await _db.SaveChangesAsync();
        }

        public async Task<CustomerModel> GetByCodeAsync(string code)
        {
            var db = await _db.Customers.FirstOrDefaultAsync(x => x.CustomerCode == code);
            var result = new CustomerModel
            {
                CustomerId = db.CustomerId,
                CustomerCode = db.CustomerCode,
                CustomerName = db.CustomerName,
                Address = db.Address,
                PhoneNo = db.PhoneNo,
            };
            return result;
        }

        public async Task<CustomerModel> GetCustomerById(int id)
        {
            return await _db.Customers
               .Where(p => p.CustomerId == id)
               .Select(p => new CustomerModel
               {
                   CustomerId = id,
                   CustomerCode = p.CustomerCode,
                   CustomerName = p.CustomerName,
                   Address = p.Address,
                   PhoneNo = p.PhoneNo,
               })
               .FirstOrDefaultAsync();
            
        }
    }
}
