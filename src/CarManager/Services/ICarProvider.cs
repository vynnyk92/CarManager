using CarManager.DTOs;

namespace CarManager.Services
{
    public interface ICarProvider
    {
        Task<string> CreateCar(CarCreateDto carCreateDto);
        Task<IReadOnlyCollection<CarGetDto>> GetCars();
    }

    public class CarProvider : ICarProvider
    {


        public Task<string> CreateCar(CarCreateDto carCreateDto)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyCollection<CarGetDto>> GetCars()
        {
            throw new NotImplementedException();
        }
    }
}
