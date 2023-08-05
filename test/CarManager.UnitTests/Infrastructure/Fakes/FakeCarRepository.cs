
using CarManager.DataAccess;
using CarManager.Models;

namespace CarManager.UnitTests.Infrastructure.Fakes
{
    internal class FakeCarRepository : ICarRepository
    {
        private readonly Dictionary<string, Car> _cars = new Dictionary<string, Car>();

        public Task<bool> CreateCar(Car car)
        {
            var result = _cars.TryAdd(car.Id, car);
            return Task.FromResult(result);
        }

        public Task<List<Car>> GetCars()
        {
            var cars = _cars.Select(c => c.Value)
                .ToList();
            return Task.FromResult(cars);
        }
    }
}
