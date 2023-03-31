using Polly;
using Polly.Extensions.Http;

namespace EventsApi.Policies
{
    public class HttpClientPolicy
    {
        public static IAsyncPolicy<HttpResponseMessage> GetExponentialRetryPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .WaitAndRetryAsync(5, (count) =>
                {
                    Console.WriteLine($"_______Handling request. Try {count}_______");
                    return TimeSpan.FromSeconds(Math.Pow(2, count));
                });
        }
    }
}
