using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using CarManager.Factories;
using CarManager.Shared;
using Microsoft.Extensions.Options;
using System.Net;
using CarManager.Models;
using Xunit;

namespace CarManager.IntegrationTests.Infrastructure.Dynamo
{
    public class DynamoFixture : IAsyncLifetime
    {
        private readonly IAmazonDynamoDB _amazonDynamoDb;

        public DynamoFixture()
        {
            var amazonDynamoDbFactory = new AmazonDynamoDbFactory(AppSettings, LocalStackSettings);
            _amazonDynamoDb = amazonDynamoDbFactory.GetAmazonDynamoDb();
        }

        public async Task InitializeAsync()
        {
            await CheckOrCreateTable(CarManagerTable.CreateTableRequest());
        }

        public async Task<IDictionary<string, AttributeValue>> GetCar(string carId)
        {
            var getItemRequest = new GetItemRequest
            {
                TableName = $"{TestSettings.Environment}-{TestSettings.TableName}".ToLowerInvariant(),
                Key = new Dictionary<string, AttributeValue>
                {
                    {nameof(Car.Id), new AttributeValue(carId)}
                }
            };
            var carResponse = await _amazonDynamoDb.GetItemAsync(getItemRequest);
            return carResponse.Item;
        }

        public async Task<bool> CleanupCar(string carId)
        {
            var deleteItemRequest = new DeleteItemRequest
            {
                TableName = $"{TestSettings.Environment}-{TestSettings.TableName}".ToLowerInvariant(),
                Key = new Dictionary<string, AttributeValue>
                {
                    {nameof(Car.Id), new AttributeValue(carId)}
                }
            };
            var deleteItem = await _amazonDynamoDb.DeleteItemAsync(deleteItemRequest);
            return (int)deleteItem.HttpStatusCode == 200;
        }

        private async Task CheckOrCreateTable(CreateTableRequest request)
        {
            // Ensure the table exists
            var tableExists = true;
            try
            {
                var res = await _amazonDynamoDb.DescribeTableAsync(new DescribeTableRequest
                {
                    TableName = request.TableName
                });
            }
            catch (ResourceNotFoundException ex)
            {
                Console.WriteLine($"{ex.Message}");
                tableExists = false;
            }

            if (!tableExists)
            {
                var result = await _amazonDynamoDb.CreateTableAsync(request);

                if (result.HttpStatusCode != HttpStatusCode.OK)
                {
                    throw new InvalidOperationException("An error occured creating the target dynamo table");
                }
            }
        }

        public Task DisposeAsync()
        {
            return Task.CompletedTask;
        }

        private IOptions<AppSettings> AppSettings =>
            Options.Create(new AppSettings
            {
                Environment = TestSettings.Environment,
                RegionEndpoint = TestSettings.RegionEndpoint,
                TableName = TestSettings.TableName
            });

        private IOptions<LocalstackSettings> LocalStackSettings =>
            Options.Create(new LocalstackSettings
            {
                Host = TestSettings.LocalstackHost,
                ProxyPort = TestSettings.ProxyPort
            });
    }
}
