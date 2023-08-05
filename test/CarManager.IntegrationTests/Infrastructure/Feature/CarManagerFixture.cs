using Xunit.Abstractions;

namespace CarManager.IntegrationTests.Infrastructure.Feature
{
    public class CarManagerFixture : IDisposable
    {
        private CarManagerFactory? _factory;
        private HttpClient? _httpClient;

        public CarManagerFixture()
        {
            CreateFactory();
            CreateClient();
        }

        public HttpClient GetHttpClient()
        {
            return _httpClient;
        }

        private void CreateFactory()
        {
            if (_factory != null || !TestSettings.IsLocalStackTesting)
                return;

            // Only start a local test server if we're running locally
            _factory = new CarManagerFactory();
        }

        private void CreateClient()
        {
            if (_httpClient != null)
                return;
            
            if (TestSettings.IsLocalStackTesting && _factory != null)
            {
                _httpClient = _factory.CreateClient();
            }
            else
            {
                _httpClient = new HttpClient
                {
                    BaseAddress = new Uri(TestSettings.FeatureUrl)
                };
            }
        }

        public void Dispose()
        {
            _httpClient.Dispose();
            _factory?.Dispose();
        }
    }
}
