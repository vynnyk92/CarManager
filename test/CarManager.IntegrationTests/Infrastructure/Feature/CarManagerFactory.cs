using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CarManager.IntegrationTests.Infrastructure.Feature
{
    public class CarManagerFactory : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            Environment.SetEnvironmentVariable(
                "environment",
                TestSettings.Environment,
                EnvironmentVariableTarget.Process);

            builder
                .UseEnvironment(Environments.Development)
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                })
                .ConfigureAppConfiguration(config =>
                {
                    config.AddInMemoryCollection(new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string>("AppSettings:Environment", TestSettings.Environment),
                        new KeyValuePair<string, string>("AppSettings:RegionEndpoint", TestSettings.RegionEndpoint),
                        new KeyValuePair<string, string>("LocalstackSettings:Host", TestSettings.LocalstackHost),
                        new KeyValuePair<string, string>("LocalstackSettings:ProxyPort", TestSettings.ProxyPort)
                    });
                });
        }
    }
}
