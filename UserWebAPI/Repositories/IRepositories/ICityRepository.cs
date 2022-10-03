using DomainModels.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ViewModels;

namespace Repositories.IRepositories
{
   public interface ICityRepository
    {
        Task<IEnumerable<CityModel>> GetAllCity();
        Task<CityModel> GetCityById(int CityId);
        Task<City> AddCity(City City);
        Task<City> UpdateCity(City City);
        Task<City> DeleteCity(int CityId);
    }
}
