using Amazon.DynamoDBv2.Model;
using Amazon.DynamoDBv2;
using CarManager.Models;

namespace CarManager.IntegrationTests.Infrastructure.Dynamo
{
    public static class CarManagerTable
    {
        public static CreateTableRequest CreateTableRequest()
        {
            return new CreateTableRequest
            {
                TableName = $"{TestSettings.Environment}-{TestSettings.TableName}".ToLowerInvariant(),
                AttributeDefinitions = new List<AttributeDefinition>
                {
                    new AttributeDefinition(nameof(Car.Id), ScalarAttributeType.S)
                },
                KeySchema = new List<KeySchemaElement>
                {
                    new KeySchemaElement(nameof(Car.Id), KeyType.HASH),

                },
                ProvisionedThroughput = new ProvisionedThroughput
                {
                    ReadCapacityUnits = 1,
                    WriteCapacityUnits = 1
                }
            };
        }
    }
}
