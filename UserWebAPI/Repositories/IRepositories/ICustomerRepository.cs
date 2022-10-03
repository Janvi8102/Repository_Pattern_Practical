using DomainModels.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ViewModels;

namespace Repositories.IRepositories
{
   public interface ICustomerRepository
    {
        Task<IEnumerable<CustomerModel>> GetAllCustomer();
        Task<CustomerModel> GetCustomerById(Guid CustomerId);
        Task<Customer> AddCustomer(Customer Customer);
        Task<Customer> UpdateCustomer(Customer Customer);
        Task<Customer> DeleteCustomer(Guid CustomerId);
    }
}
