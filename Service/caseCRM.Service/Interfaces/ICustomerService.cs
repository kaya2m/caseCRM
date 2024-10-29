using caseCRM.DataAccess.Common;
using caseCRM.Entities.DTOs;
using caseCRM.Entities.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace caseCRM.Service.Interfaces
{
    public interface ICustomerService  
    {
        Task<ResponseDto<CustomerDto>> GetCustomerByIdAsync(string customerId);
        Task<ResponseDto<List<CustomerDto>>> GetAllCustomersAsync();
        Task<ResponseDto<List<CustomerDto>>> SearchCustomersAsync(string searchQuery);
        Task<ResponseDto<List<CustomerDto>>> GetAllCustomerByRegionAsync(string region);
        Task<ResponseDto<EmptyDto>> CreateCustomerAsync(CustomerDto customerDto);
        Task<ResponseDto<EmptyDto>> UpdateCustomerAsync(string customerId, CustomerDto customerDto);
        Task<ResponseDto<EmptyDto>> DeleteCustomerAsync(string customerId);
      
    }
}
