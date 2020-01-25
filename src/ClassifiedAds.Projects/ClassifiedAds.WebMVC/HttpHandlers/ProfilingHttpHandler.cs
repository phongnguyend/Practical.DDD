using StackExchange.Profiling;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace ClassifiedAds.WebMVC.HttpHandlers
{
    public class ProfilingHttpHandler : DelegatingHandler
    {
        public ProfilingHttpHandler()
        {
        }

        public ProfilingHttpHandler(HttpMessageHandler innerHandler)
        {
            InnerHandler = innerHandler;
        }


        protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var httpVerb = request.Method.ToString();
            string url = request.RequestUri.AbsoluteUri;

            var requestContent = await GetRequestContent(request);

            using (CustomTiming timing = MiniProfiler.Current.CustomTiming("http", string.Empty, httpVerb))
            {
                var response = await base.SendAsync(request, cancellationToken);

                timing.CommandString = $"URL          : {url}\nCONTENT      : {requestContent}\nRESPONSE CODE: {response.StatusCode}";
                return response;
            }
        }


        internal async Task<string> GetRequestContent(HttpRequestMessage request)
        {
            if (request.Content != null)
            {
                return await request.Content.ReadAsStringAsync();
            }
            else
            {
                return await Task.FromResult(string.Empty);
            }
        }

    }
}
