using Microsoft.EntityFrameworkCore;
using YuNilarThinn.MiniPOS.AppDbContextModels;

namespace YuNilarThinn.MiniPOS
{
    public class DevCode
    {
        private readonly AppDbContext _db;
        public DevCode(AppDbContext db)
        {
            _db = db;
        }
        public async Task<string?> GetCustomerCode()
        {
          
            var lastCustomer = await _db.Customers
        .OrderByDescending(c => c.CustomerCode)
        .FirstOrDefaultAsync();

            int nextNumber = 1;

            if (lastCustomer != null)
            {
                string lastCode = lastCustomer.CustomerCode; 
                if (int.TryParse(lastCode.Split('-').Last(), out int codeNum))
                {
                    nextNumber = codeNum + 1;
                }
            }

            return $"C-{nextNumber.ToString("D4")}";
        }
    }
}
