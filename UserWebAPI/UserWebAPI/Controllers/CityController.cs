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
    public class CityController : ControllerBase
    {
        private readonly ICityRepository _cityRepository;

        public CityController(ICityRepository cityRepository)
        {
            _cityRepository = cityRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCity()
        {
            try
            {
                var city = await _cityRepository.GetAllCity();
                return Ok(city);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error in Retrieving Data from Database");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<CityModel>> GetCityById(int id)
        {
            try
            {
                var result = await _cityRepository.GetCityById(id);
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
        public async Task<ActionResult<City>> AddCity(City city)
        {
            try
            {
                await _cityRepository.AddCity(city);
                return CreatedAtAction("GetCityById", new { id = city.CityId }, city);
            }
            catch (KeyNotFoundException)
            {
                return StatusCode(StatusCodes.Status409Conflict, "State doesn't Exist");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status409Conflict, "City already Exist");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<City>> UpdateCity(int id, City city)
        {
            try
            {
                if (id != city.CityId)
                {
                    return BadRequest("Id is Mismatched");
                }
                var CityUpdate = await _cityRepository.GetCityById(id);

                if (CityUpdate == null)
                {
                    return NotFound($"City Id = {id} not found");
                }

                return await _cityRepository.UpdateCity(city);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error in Retriving Data from Database"); ;
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<City>> DeleteCity(int id)
        {
            try
            {
                var cityDelete = await _cityRepository.GetCityById(id);

                if (cityDelete == null)
                {
                    return NotFound($"City Id = {id} not found");
                }

                return await _cityRepository.DeleteCity(id);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error in Retriving Data from Database"); ;
            }
        }
    }
}
