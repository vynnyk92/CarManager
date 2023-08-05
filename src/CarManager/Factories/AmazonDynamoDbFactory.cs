using Amazon;
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
        private readonly LocalstackSettings _localstackSettings;

        public AmazonDynamoDbFactory(
            IOptions<AppSettings> appSettings, 
            IOptions<LocalstackSettings> localstackSettings)
        {
            _localstackSettings = localstackSettings.Value;
            _appSettings = appSettings.Value;
        }

        public IAmazonDynamoDB GetAmazonDynamoDb()
        {
            if (EnvironmentHelper.IsLocal(_appSettings.Environment))
            {
                return new AmazonDynamoDBClient(
                    new AmazonDynamoDBConfig
                    {
                        ServiceURL = $"https://{_localstackSettings.Host}:{_localstackSettings.ProxyPort}",
                        RegionEndpoint = RegionEndpoint.GetBySystemName(_appSettings.RegionEndpoint)
                    });
            }

            return new AmazonDynamoDBClient(
                new AmazonDynamoDBConfig
                {
                    RegionEndpoint = RegionEndpoint.GetBySystemName(_appSettings.RegionEndpoint)
                });
        }
    }
}

