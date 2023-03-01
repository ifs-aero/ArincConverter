using System.Net.Http.Headers;

namespace ArincConverter.Helpers
{
    public class AuthenticatedHttpClientHandler : HttpClientHandler
    {
        private readonly string _token;
        
        public AuthenticatedHttpClientHandler(string token)
        {
            _token = token;
        }
        
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _token);

            return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
        }
    }
}
