using AutoMapper;
using CarManager.DataAccess;
using CarManager.Services;
using CarManager.UnitTests.Infrastructure.Fakes;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit;
using AutoFixture;
using CarManager.DTOs;
using Shouldly;
using CarManager.Models;

namespace CarManager.UnitTests.Services
{
    public class CarProviderTest
    {
        private readonly ICarProvider _carProvider;
        private readonly ICarRepository _carRepo;
        private readonly Fixture _autoFixture = new ();

        public CarProviderTest()
        {
            _carRepo = new FakeCarRepository();

            var config = new MapperConfiguration(opts =>
            {
                // Add your mapper profile configs or mappings here
                opts.CreateMap<CarCreateDto, Car>();
                opts.CreateMap<Car, CarGetDto>();
            });

            var mapper = config.CreateMapper();

            _carProvider = new CarProvider(_carRepo,
                NullLogger<CarProvider>.Instance,
                mapper);
        }

        [Fact]
        public async Task CreateCar_WhenAddCar_ItAppearsInRepo()
        {
            //Arrange
            var carDto = _autoFixture.Create<CarCreateDto>();

            //Act
            var id = await _carProvider.CreateCar(carDto);

            //Assert
            var car = _carRepo.GetCars()
                .Result
                .FirstOrDefault(c => c.Id.Equals(id, StringComparison.InvariantCultureIgnoreCase));

            car.ShouldNotBeNull();
        }
    }
}
