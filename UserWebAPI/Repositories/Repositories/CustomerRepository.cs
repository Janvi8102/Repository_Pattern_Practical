using AutoMapper;
using DomainModels;
using DomainModels.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels;

namespace Repositories.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly AppDbContext _Context;
        private readonly IMapper _mapper;
        public CustomerRepository(AppDbContext Context, IMapper mapper)
        {
            _Context = Context;
            _mapper = mapper;
        }

        public async Task<Customer> AddCustomer(Customer Customer)
        {
            if (_Context.Customers.Any(r => r.CustomerName == Customer.CustomerName))
            {
                throw new Exception("Customer Name Alreaady Exist");
            }
            else if (_Context.Customers.Any(r => r.Mobile == Customer.Mobile))
            {
                throw new Exception("Mobile number Alreaady Exist");
            }
            else if (_Context.Customers.Any(r => r.Email == Customer.Email))
            {
                throw new Exception("EmailId Alreaady Exist");
            }
            else if (_Context.City.Any(r => r.CityId == Customer.CityId))
            {
                var result = await _Context.Customers.AddAsync(Customer);
                await _Context.SaveChangesAsync();
                return result.Entity;
            }
            else
            {
                throw new KeyNotFoundException();
            }
        }

        public async Task<Customer> DeleteCustomer(Guid CustomerId)
        {
            var result = await _Context.Customers.Where(a => a.CustomerId == CustomerId).FirstOrDefaultAsync();
            if (result != null)
            {
                _Context.Customers.Remove(result);
                await _Context.SaveChangesAsync();
                return result;
            }
            return null;
        }

        public async Task<IEnumerable<CustomerModel>> GetAllCustomer()
        {
            var customer = await _Context.Customers.Include(x => x.City).ThenInclude(x => x.State).ThenInclude(x => x.Country).ToListAsync();
            return _mapper.Map<List<CustomerModel>>(customer);
        }

        public async Task<CustomerModel> GetCustomerById(Guid CustomerId)
        {
            var records = await _Context.Customers.Include(x => x.City).ThenInclude(x => x.State).ThenInclude(x => x.Country).FirstOrDefaultAsync(a => a.CustomerId == CustomerId);
            return _mapper.Map<CustomerModel>(records);
        }

        public async Task<Customer> UpdateCustomer(Customer Customer)
        {
            var result = await _Context.Customers.FirstOrDefaultAsync(a => a.CustomerId == Customer.CustomerId);
            if (result != null)
            {
                result.CustomerName = Customer.CustomerName;
                result.Email = Customer.Email;
                result.Address1 = Customer.Address1;
                result.Address2 = Customer.Address2;
                result.Address3 = Customer.Address3;
                result.PinCode = Customer.PinCode;
                result.Mobile = Customer.Mobile;
                result.CityId = Customer.CityId;
                await _Context.SaveChangesAsync();
                return result;
            }
            return null;
        }
    }
}
