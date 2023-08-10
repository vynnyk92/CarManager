using AutoFixture;
using CarManager.DTOs;
using CarManager.IntegrationTests.Infrastructure.Dynamo;
using CarManager.IntegrationTests.Infrastructure.Feature;
using Newtonsoft.Json;
using System.Text;
using Shouldly;
using Xunit;
using CarManager.Models;

namespace CarManager.IntegrationTests.Infrastructure.Tests
{
    [Collection(nameof(CarManagerCollectionFixture))]
    public class CreateCarTest : IDisposable
    {
        private readonly HttpClient _httpClient;
        private readonly DynamoFixture _dynamoFixture;
        private readonly Fixture _fixture = new ();
        private string _carId;

        public CreateCarTest(
            CarManagerFixture carManagerFixture,
            DynamoFixture dynamoFixture
            )
        {
            _httpClient = carManagerFixture.GetHttpClient();
            _dynamoFixture = dynamoFixture;
        }

        [Fact]
        public async Task CreateCarRequest_ShouldSuccessfullyCreateCar()
        {
            //Arrange
            var car = _fixture.Create<CarCreateDto>();
            var payload = JsonConvert.SerializeObject(car);
            var carCreateContent = new StringContent(payload, Encoding.UTF8, "application/json");

            //Act
            var response = await _httpClient.PostAsync("/car", carCreateContent);
            var carId = await response.Content.ReadAsStringAsync();
            _carId = carId;

            //Assert
            response.IsSuccessStatusCode.ShouldBe(true);

            var carFromDb = await _dynamoFixture.TryGetCar(carId);
            carFromDb.ShouldNotBeNull();
            carFromDb[nameof(Car.Frame)].S.ShouldBe(car.Frame);
        }

        public void Dispose()
        {
            Task.Run(() => _dynamoFixture.CleanupCar(_carId));
        }
    }
}
