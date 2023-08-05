using AutoMapper;
using CarManager.DataAccess;
using CarManager.DTOs;
using CarManager.Models;

namespace CarManager.Services
{
    public interface ICarProvider
    {
        Task<string> CreateCar(CarCreateDto carCreateDto);
        Task<IReadOnlyCollection<CarGetDto>> GetCars();
    }

    public class CarProvider : ICarProvider
    {
        private readonly ICarRepository _carRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CarProvider> _logger;

        public CarProvider(ICarRepository carRepository, ILogger<CarProvider> logger, IMapper mapper)
        {
            _carRepository = carRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<string> CreateCar(CarCreateDto carCreateDto)
        {
            var car = _mapper.Map<Car>(carCreateDto);
            //Probably should be addressed to id generation service
            car.Id = Guid.NewGuid().ToString();

            var isCarCreated = await _carRepository.CreateCar(car);
            return isCarCreated ? car.Id : throw new Exception();
        }

        public async Task<IReadOnlyCollection<CarGetDto>> GetCars()
        {
            var cars = await _carRepository.GetCars();
            var carDtos = _mapper.Map<IReadOnlyCollection<CarGetDto>>(cars);
            return carDtos;
        }
    }
}
