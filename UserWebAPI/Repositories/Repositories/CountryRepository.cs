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
    public class CountryRepository : ICountryRepository
    {
        private readonly AppDbContext _Context;
        private readonly IMapper _mapper;
        public CountryRepository(AppDbContext Context, IMapper mapper)
        {
            _Context = Context;
            _mapper = mapper;
        }

        public async Task<Country> AddCountry(Country country)
        {   
            if (_Context.Countries.Any(r => r.CountryName == country.CountryName))
            {
                throw new Exception();
            }
            var result = await _Context.Countries.AddAsync(country);
            await _Context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<IEnumerable<CountryModel>> GetAllCountry()
        {
            var countries = await _Context.Countries.ToListAsync();
            return _mapper.Map<List<CountryModel>>(countries);
        }

        public async Task<CountryModel> GetCountryById(int CountryId)
        {
            var records = await _Context.Countries.FirstOrDefaultAsync(a => a.CountryId == CountryId);
            return _mapper.Map<CountryModel>(records);
        }

        public async Task<Country> UpdateCountry(Country country)
        {
            var result = await _Context.Countries.FirstOrDefaultAsync(a => a.CountryId == country.CountryId);
            if (result != null)
            {
                result.CountryName = country.CountryName;
                await _Context.SaveChangesAsync();
                return result;
            }
            return null;
        }

        public async Task<Country> DeleteCountry(int CountryId)
        {
            var result = await _Context.Countries.Where(a => a.CountryId == CountryId).FirstOrDefaultAsync();
            if (result != null)
            {
                _Context.Countries.Remove(result);
                await _Context.SaveChangesAsync();
                return result;
            }
            return null;
        }
    }
}
