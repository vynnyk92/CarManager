using System.Net;

namespace CarManager.Extensions
{
    public static class HttpStatusCodeExtensions
    {
        public static bool IsSuccessfulStatusCode(this HttpStatusCode code)
        {
            var codeAsInt = (int)code;
            return codeAsInt >= 200 && codeAsInt < 300;
        }
    }
}
