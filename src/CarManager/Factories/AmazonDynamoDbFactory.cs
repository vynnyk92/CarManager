using Amazon.DynamoDBv2;
using CarManager.Helpers;
using CarManager.Shared;
using Microsoft.Extensions.Options;

namespace CarManager.Factories
{
    public interface IAmazonDynamoDbFactory
    {
        IAmazonDynamoDB GetAmazonDynamoDb();
    }

    public class AmazonDynamoDbFactory : IAmazonDynamoDbFactory
    {
        private readonly AppSettings _appSettings;

        public AmazonDynamoDbFactory(
            IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public IAmazonDynamoDB GetAmazonDynamoDb()
        {
            if (EnvironmentHelper.IsLocal(_appSettings.Environment))
            {
                return new AmazonDynamoDBClient(
                    new AmazonDynamoDBConfig
                    {
                        ServiceURL = $"https://{LocalStackHostHelper.GetLocalStackHost()}:{_awsClientsSettings.ProxyPort}",
                        HttpClientFactory = new LocalHttpClientFactory(),
                        MaxErrorRetry = _awsClientsSettings.MaxErrorRetry,
                        Timeout = TimeSpan.FromSeconds(_awsClientsSettings.Timeout),
                        RegionEndpoint = RegionEndpoint.GetBySystemName(_awsClientsSettings.InfrastructureRegion)
                    });
            }

            return new AmazonDynamoDBClient(
                new AmazonDynamoDBConfig
                {
                    RegionEndpoint = RegionEndpoint.GetBySystemName(_awsClientsSettings.InfrastructureRegion),
                    MaxErrorRetry = _awsClientsSettings.MaxErrorRetry,
                    Timeout = TimeSpan.FromSeconds(_awsClientsSettings.Timeout)
                });
        }
    }
}

