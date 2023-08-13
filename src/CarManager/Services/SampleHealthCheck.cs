using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace CarManager.Services
{
    public class SampleHealthCheckWithArgs : IHealthCheck
    {
        private readonly int _arg1;
        private readonly string _arg2;

        public SampleHealthCheckWithArgs()
        {
        }

        public Task<HealthCheckResult> CheckHealthAsync(
            HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(HealthCheckResult.Healthy("A healthy result."));
        }
    }
}
