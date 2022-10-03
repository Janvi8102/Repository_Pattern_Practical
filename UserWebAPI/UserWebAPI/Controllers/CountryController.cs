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
    public class CountryController : ControllerBase
    {
        private readonly ICountryRepository _countryRepository;

        public CountryController(ICountryRepository countryRepository)
        {
            _countryRepository = countryRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCountry()
        {
            try
            {
                return Ok(await _countryRepository.GetAllCountry());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error in Retrieving Data from Database");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Country>> AddCountry(Country country)
        {
            try
            {
                var CreateCountry = await _countryRepository.AddCountry(country);
                return CreatedAtAction("GetCountryById", new { id = country.CountryId }, country);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status409Conflict, "CountryName Already Exist");
            }
        }


        [HttpGet("{id:int}")]
        public async Task<ActionResult<CountryModel>> GetCountryById(int id)
        {
            try
            {
                var result = await _countryRepository.GetCountryById(id);
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

        [HttpPut("{id:int}")]
        public async Task<ActionResult<Country>> UpdateCountry(int id, Country country)
        {
            try
            {
                if(id != country.CountryId)
                {
                    return BadRequest("Id is Mismatched");
                }
                var countryUpdate = await _countryRepository.GetCountryById(id);
               
                if(countryUpdate == null)
                {
                    return NotFound($"Country Id = {id} not found");
                }

                return await _countryRepository.UpdateCountry(country);              
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error in Retriving Data from Database"); ;
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Country>> DeleteCountry(int id)
        {
            try
            {
                var countryDelete = await _countryRepository.GetCountryById(id);

                if (countryDelete == null)
                {
                    return NotFound($"Country Id = {id} not found");
                }

                return await _countryRepository.DeleteCountry(id);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error in Retriving Data from Database"); ;
            }
        }

    }
}
