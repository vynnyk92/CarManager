using CarManager.Shared;

namespace CarManager.Helpers
{
    public class EnvironmentHelper
    {
        public static bool IsLocal(string environment) =>
            EnvironmentConstants.Development.Equals(environment, StringComparison.InvariantCultureIgnoreCase);
    }
}
