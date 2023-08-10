using Amazon.DynamoDBv2.Model;
using CarManager.Models;
using Polly;

namespace CarManager.IntegrationTests.Infrastructure.Dynamo
{
    public static class DynamoFixtureRetryExtension
    {
        public static async Task<IDictionary<string, AttributeValue>> TryGetCar(this DynamoFixture dynamoFixture, string carId)
        {
            var result = await Policy.HandleResult<IDictionary<string, AttributeValue>>(
                i=> i.ContainsKey(nameof(Car.Id)) && i[nameof(Car.Id)].S == carId)
                .WaitAndRetryAsync(10, _ => TimeSpan.FromSeconds(1))
                .ExecuteAsync(() => dynamoFixture.GetCar(carId));

            return result;
        }
    }
}
