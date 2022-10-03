using AutoMapper;
using DomainModels.InPutModel;
using DomainModels.Models;
using System;
using ViewModels;

namespace Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Country, CountryModel>().ReverseMap();
            CreateMap<State, StateModel>().ReverseMap();          
            CreateMap<City, CityModel>().ReverseMap();
            CreateMap<Customer, CustomerModel>().ReverseMap();
        }
    }
}
