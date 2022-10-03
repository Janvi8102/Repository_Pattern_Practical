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
    public class CityRepository : ICityRepository
    {
        private readonly AppDbContext _Context;
        private readonly IMapper _mapper;
        public CityRepository(AppDbContext Context, IMapper mapper)
        {
            _Context = Context;
            _mapper = mapper;
        }

        public async Task<City> AddCity(City city)
        {
            if(_Context.City.Any(r => r.CityName == city.CityName))
            {
                throw new Exception();
            }
            else if(_Context.States.Any(r => r.StateId == city.StateId))
            {
                var result = await _Context.City.AddAsync(city);
                await _Context.SaveChangesAsync();
                return result.Entity;
            }
            else
            {
                throw new KeyNotFoundException();
            }
        }

        public async Task<City> DeleteCity(int cityId)
        {
            var result = await _Context.City.Where(a => a.CityId == cityId).FirstOrDefaultAsync();
            if (result != null)
            {
                _Context.City.Remove(result);
                await _Context.SaveChangesAsync();
                return result;
            }
            return null;
        }

        public async Task<IEnumerable<CityModel>> GetAllCity()
        {
            var city = await _Context.City.Include(x => x.State).ThenInclude(x => x.Country).ToListAsync();
            return _mapper.Map<List<CityModel>>(city);
        }

        public async Task<CityModel> GetCityById(int cityId)
        {
            var records = await _Context.City.Include(x => x.State).ThenInclude(x => x.Country).FirstOrDefaultAsync(a => a.CityId == cityId);
            return _mapper.Map<CityModel>(records);
        }

        public async Task<City> UpdateCity(City city)
        {
            var result = await _Context.City.FirstOrDefaultAsync(a => a.CityId == city.CityId);
            if (result != null)
            {
                result.CityName = city.CityName;
                result.StateId = city.StateId;
                await _Context.SaveChangesAsync();
                return result;
            }
            return null;
        }
    }
}
