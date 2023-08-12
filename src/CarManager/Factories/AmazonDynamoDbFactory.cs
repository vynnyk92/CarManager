using System.Net;
using Amazon;
using Amazon.DynamoDBv2;
using Amazon.Runtime;
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
                var credentials = new BasicAWSCredentials("test1", "test2");

                return new AmazonDynamoDBClient(credentials,
                    new AmazonDynamoDBConfig
                    {
                        ServiceURL = $"http://{_localstackSettings.Host}:{_localstackSettings.ProxyPort}"
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

