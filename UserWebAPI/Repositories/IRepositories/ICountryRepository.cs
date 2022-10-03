using DomainModels.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ViewModels;

namespace Repositories.IRepositories
{
    public interface ICountryRepository 
    {
        Task<IEnumerable<CountryModel>> GetAllCountry();
        Task<CountryModel> GetCountryById(int CountryId);
        Task<Country> AddCountry(Country country);
        Task<Country> UpdateCountry(Country country);
        Task<Country> DeleteCountry(int CountryId);
    }
}
