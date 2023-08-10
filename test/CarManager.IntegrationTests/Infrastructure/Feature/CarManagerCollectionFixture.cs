using CarManager.IntegrationTests.Infrastructure.Dynamo;
using Xunit;

namespace CarManager.IntegrationTests.Infrastructure.Feature
{
    [CollectionDefinition(nameof(CarManagerCollectionFixture))]
    public class CarManagerCollectionFixture : ICollectionFixture<CarManagerFixture>,
        ICollectionFixture<DynamoFixture>
    {
    }
}
