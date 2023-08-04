using AutoMapper;
using CarManager.DTOs;
using CarManager.Models;

namespace CarManager.Shared
{
    public class CarProfile : Profile
    {
        public CarProfile()
        {
            CreateMap<CarCreateDto, Car>();
            CreateMap<Car, CarGetDto>();
        }
    }
}
