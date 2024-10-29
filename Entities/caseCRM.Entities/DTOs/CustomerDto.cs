using caseCRM.Entities.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace caseCRM.Entities.DTOs
{
    public record class CustomerDto
    {
        public Guid Id { get; set; }
        public string Firstname { get; init;}
        public string Lastname { get; init;}
        public string Email { get; init;}
        public string Region { get; init;}
        public bool Active { get; init;}

        public static explicit operator CustomerDto(Customer customer)
        {
            return new CustomerDto
            {
                Id = customer.id,
                Firstname = customer.firstname,
                Lastname = customer.lastname,
                Email = customer.email,
                Region = customer.region,
                Active = customer.active
            };
        }
    }
}
