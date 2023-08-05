using CarManager.DataAccess;
using CarManager.Factories;
using CarManager.Shared;

namespace CarManager.Extensions
{
    public static class DynamoDbServiceCollectionExtensions
    {
        public static IServiceCollection AddDynamoDb(this IServiceCollection services, IConfiguration configuration)
        {
            var config = configuration.GetSection("AppSettings").Get<AppSettings>();

            services.AddSingleton<IAmazonDynamoDbFactory, AmazonDynamoDbFactory>();
            services.AddSingleton(sp =>
                sp.GetRequiredService<IAmazonDynamoDbFactory>().GetAmazonDynamoDb());

            services.AddSingleton<ICarRepository, DynamoCarRepository>();

            return services;
        }
    }
}
