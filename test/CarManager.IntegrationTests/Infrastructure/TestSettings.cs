using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace CarManager.IntegrationTests.Infrastructure
{
    public static class TestSettings
    {
        public const string Development = nameof(Development);
        public static string LocalStack = nameof(LocalStack);

        private static readonly IConfiguration _configuration;

        static TestSettings()
        {
            var environment = System.Environment.GetEnvironmentVariable("environment");
            if (string.IsNullOrEmpty(environment))
                environment = Environments.Development;

            _configuration = new ConfigurationBuilder()
                .AddJsonFile("./appsettings.json")
                .AddJsonFile($"./appsettings.{environment}.json", true)
                .Build();
        }

        public static string Environment =>
            _configuration[nameof(Environment)];

        public static bool IsLocalStackTesting =>
            Environment.Equals(LocalStack, StringComparison.InvariantCultureIgnoreCase);

        public static string LocalstackHost => 
            _configuration[nameof(LocalstackHost)];

        public static string ProxyPort =>
            _configuration[nameof(ProxyPort)];

        public static string RegionEndpoint =>
            _configuration[nameof(RegionEndpoint)];

        public static string FeatureUrl =>
            _configuration[nameof(FeatureUrl)];

        public static string TableName =>
            _configuration[nameof(TableName)];
    }
}
