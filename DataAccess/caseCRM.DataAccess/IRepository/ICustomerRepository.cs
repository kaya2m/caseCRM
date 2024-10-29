using caseCRM.DataAccess.Common;
using caseCRM.Entities.DTOs;
using caseCRM.Entities.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace caseCRM.DataAccess.IRepository
{
    public interface ICustomerRepository : IGenericRepository<Customer>
    {
        Task<ResponseDto<Customer>> GetAllCustomerByRegionAsync(string region);
    }
}
