using DomainModels.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ViewModels;

namespace UserWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : Controller
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCustomer()
        {
            try
            {
                var customer = await _customerRepository.GetAllCustomer();
                return Ok(customer);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error in Retrieving Data from Database");
            }
        }

        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<CustomerModel>> GetCustomerById(Guid id)
        {
            try
            {
                var result = await _customerRepository.GetCustomerById(id);
                if (result == null)
                {
                    return NotFound();
                }
                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error in Retrieving Data from Database");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Customer>> AddCustomer(Customer customer)
        {
            try
            {
                await _customerRepository.AddCustomer(customer);
                return CreatedAtAction("GetAllCustomer", new { id = customer.CustomerId }, customer);
            }
            catch (KeyNotFoundException)
            {
                return StatusCode(StatusCodes.Status409Conflict, "City doesn't Exist");
            }
        }

        [HttpPut("{id:Guid}")]
        public async Task<ActionResult<Customer>> UpdateCustomer(Guid id, Customer customer)
        {
            try
            {
                if (id != customer.CustomerId)
                {
                    return BadRequest("Id is Mismatched");
                }
                var CustomerUpdate = await _customerRepository.GetCustomerById(id);

                if (CustomerUpdate == null)
                {
                    return NotFound($"Customer Id = {id} not found");
                }

                return await _customerRepository.UpdateCustomer(customer);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error in Retriving Data from Database"); ;
            }
        }

        [HttpDelete("{id:Guid}")]
        public async Task<ActionResult<Customer>> DeleteCustomer(Guid id)
        {
            try
            {
                var cityDelete = await _customerRepository.GetCustomerById(id);

                if (cityDelete == null)
                {
                    return NotFound($"Customer Id = {id} not found");
                }

                return await _customerRepository.DeleteCustomer(id);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error in Retriving Data from Database"); ;
            }
        }
    }
}
