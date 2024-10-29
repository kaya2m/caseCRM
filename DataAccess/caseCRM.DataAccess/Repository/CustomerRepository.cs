using caseCRM.DataAccess.Common;
using caseCRM.DataAccess.IRepository;
using caseCRM.Entities.DTOs;
using caseCRM.Entities.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace caseCRM.DataAccess.Repository
{
    public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
    {
        private readonly AppDbContext _context;

        public CustomerRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<ResponseDto<Customer>> GetAllCustomerByRegionAsync(string region)
        {
            try
            {
                var customers = await _context.Customers
                    .Where(c => c.region == region) 
                    .AsNoTracking()
                    .ToListAsync(); 

                return ResponseDto<Customer>.Success(customers,200);
            }
            catch (Exception ex)
            {
                return ResponseDto<Customer>.Fail(ex.Message, 500, false);
            }
        }
    }
}
