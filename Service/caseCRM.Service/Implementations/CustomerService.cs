using caseCRM.DataAccess;
using caseCRM.DataAccess.Common;
using caseCRM.DataAccess.IRepository;
using caseCRM.Entities.DTOs;
using caseCRM.Entities.Entity;
using caseCRM.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace caseCRM.Service.Implementations
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<ResponseDto<EmptyDto>> CreateCustomerAsync(CustomerDto customerDto)
        {
            try
            {
                var customer = new Customer
                {
                    firstname = customerDto.Firstname,
                    lastname = customerDto.Lastname,
                    email = customerDto.Email,
                    region = customerDto.Region,
                    active = customerDto.Active
                };
                await _customerRepository.AddAsync(customer);
                await _customerRepository.SaveAsync();

                return ResponseDto<EmptyDto>.Success("Customer created successfully", 201);
            }
            catch (Exception ex)
            {
                return ResponseDto<EmptyDto>.Fail(ex.Message, 500);
            }
        }
        public async Task<ResponseDto<EmptyDto>> UpdateCustomerAsync(string customerId, CustomerDto customerDto)
        {
            try
            {
                var customer = await _customerRepository.GetByIdAsync(customerId);
                if (customer == null)
                {
                    return ResponseDto<EmptyDto>.Fail("Customer not found", 404);
                }

                customer.Data.firstname = customerDto.Firstname;
                customer.Data.lastname = customerDto.Lastname;
                customer.Data.email = customerDto.Email;
                customer.Data.region = customerDto.Region;
                customer.Data.active = customerDto.Active;

                await _customerRepository.UpdateAsync(customer.Data);
                await _customerRepository.SaveAsync();

                return ResponseDto<EmptyDto>.Success("Customer updated successfully", 200);
            }
            catch (Exception ex)
            {
                return ResponseDto<EmptyDto>.Fail(ex.Message, 500);
            }
        }


        public async Task<ResponseDto<EmptyDto>> DeleteCustomerAsync(string customerId)
        {
            try
            {
                var customer = await _customerRepository.GetByIdAsync(customerId);
                if (customer == null)
                {
                    return ResponseDto<EmptyDto>.Fail("Customer not found", 404);
                }

                await _customerRepository.SoftRemoveAsync(customer.Data.id);
                await _customerRepository.SaveAsync();

                return ResponseDto<EmptyDto>.Success("Customer deleted successfully", 200);
            }
            catch (Exception ex)
            {
                return ResponseDto<EmptyDto>.Fail(ex.Message, 500);
            }
        }

        public async Task<ResponseDto<List<CustomerDto>>> GetAllCustomersAsync()
        {
            try
            {
                var customers = await _customerRepository.GetAll();
                var customerDtos = customers.Data.Select(c => (CustomerDto)c).ToList();
                return ResponseDto<List<CustomerDto>>.Success(customerDtos, "Customers retrieved successfully", 200);
            }
            catch (Exception ex)
            {
                return ResponseDto<List<CustomerDto>>.Fail(ex.Message, 500);
            }
        }

        public async Task<ResponseDto<CustomerDto>> GetCustomerByIdAsync(string customerId)
        {
            try
            {
                var customer = await _customerRepository.GetByIdAsync(customerId);
                if (customer == null)
                {
                    return ResponseDto<CustomerDto>.Fail("Customer not found", 404);
                }

                var customerDto = (CustomerDto)customer.Data;
                return ResponseDto<CustomerDto>.Success(customerDto, "Customer retrieved successfully", 200);
            }
            catch (Exception ex)
            {
                return ResponseDto<CustomerDto>.Fail(ex.Message, 500);
            }
        }

        public async Task<ResponseDto<List<CustomerDto>>> SearchCustomersAsync(string searchQuery)
        {
            try
            {
                var customers = await _customerRepository.GetWhereAsync(c =>
                    c.region.Contains(searchQuery) ||
                    c.lastname.Contains(searchQuery) ||
                    c.email.Contains(searchQuery));

                var customerDtos = customers.Data.Select(c => (CustomerDto)c).ToList();
                return ResponseDto<List<CustomerDto>>.Success(customerDtos, "Search completed successfully", 200);
            }
            catch (Exception ex)
            {
                return ResponseDto<List<CustomerDto>>.Fail(ex.Message, 500);
            }
        }

        public async Task<ResponseDto<List<CustomerDto>>> GetAllCustomerByRegionAsync(string region)
        {
            try
            {
                var customer = await _customerRepository.GetWhereAsync(c=>c.region == region);
                if (customer == null)
                {
                    return ResponseDto<List<CustomerDto>>.Fail("Customer not found", 404);
                }

                var customerDto = (CustomerDto)customer.Data;
                return ResponseDto<List<CustomerDto>>.Success(customerDto, "Customer retrieved successfully", 200);
            }
            catch (Exception ex)
            {
                return ResponseDto<List<CustomerDto>>.Fail(ex.Message, 500);
            }
        }
    }
}
