using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using CarManager.Extensions;
using CarManager.Models;
using CarManager.Shared;
using Microsoft.Extensions.Options;

namespace CarManager.DataAccess
{
    public interface ICarRepository
    {
        Task<bool> CreateCar(Car car);
        Task<List<Car>> GetCars();
    }

    public class DynamoCarRepository : ICarRepository
    {
        private readonly IAmazonDynamoDB _amazonDynamoDb;
        private readonly ILogger<DynamoCarRepository> _logger;
        private readonly AppSettings _appSettings;

        public DynamoCarRepository(IAmazonDynamoDB amazonDynamoDb, 
            ILogger<DynamoCarRepository> logger, 
            IOptions<AppSettings> appSettings)
        {
            _amazonDynamoDb = amazonDynamoDb;
            _logger = logger;
            _appSettings = appSettings.Value;
        }

        public async Task<bool> CreateCar(Car car)
        {
            var request = new PutItemRequest
            {
                TableName = TableName,
                Item = new Dictionary<string, AttributeValue>
                {
                    {nameof(Car.Id), new AttributeValue(car.Id) },
                    {nameof(Car.Brand), new AttributeValue(car.Brand) },
                    {nameof(Car.Type), new AttributeValue(car.Type) },
                    {nameof(Car.Frame), new AttributeValue(car.Frame) },
                    {nameof(Car.Description), new AttributeValue(car.Description) },
                    {nameof(Car.PictureReference), new AttributeValue(car.PictureReference) },
                }
            };

            var response = await _amazonDynamoDb.PutItemAsync(request);
            return response.HttpStatusCode.IsSuccessfulStatusCode();
        }

        public Task<List<Car>> GetCars()
        {
            throw new NotImplementedException();
        }

        private string TableName =>
            $"{_appSettings.Environment}-{_appSettings.TableName}".ToLowerInvariant();
    }
}
