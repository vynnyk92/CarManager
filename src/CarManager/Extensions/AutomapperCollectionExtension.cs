using AutoMapper;

namespace CarManager.Extensions
{
    public static class AutoMapperCollectionExtension
    {
        public static IServiceCollection AddAutoMapper(this IServiceCollection serviceCollection)
        {

            serviceCollection
                .AddAutoMapper(typeof(Program).Assembly);

            return serviceCollection;
        }
    }
}
