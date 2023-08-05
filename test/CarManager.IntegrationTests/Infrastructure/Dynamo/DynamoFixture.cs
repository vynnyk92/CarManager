using Amazon.DynamoDBv2;
using CarManager.Factories;
using CarManager.Shared;
using Microsoft.Extensions.Options;
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

        public Task InitializeAsync()
        {
            throw new NotImplementedException();
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
