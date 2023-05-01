using ArincConverter.Contracts;
using ArincConverter.Helpers;
using ArincConverter.Models;
using Refit;
using System.Text.RegularExpressions;

namespace ArincConverter.Services
{
    public class EfbApiService : IEfbApiService
    {
        private IEfbApi _efbApi;

        public async Task<string> Login(LoginRequest request, string environment)
        {
            Console.WriteLine("\n\nAuthenticating...");
            InitRestClient(environment);

            var response = await _efbApi.Login(request, CancellationToken.None);
            return response.Token;
        }

        public async Task<int> PostFlightPlan(FlightPlan flightPlan, string token, string environment)
        {
            InitRestClient(environment, token);

            Console.WriteLine("\nPosting flight plan...");
            var response = await _efbApi.PostFlightPlan(new[] { flightPlan });

            await using var stream = await response.Content.ReadAsStreamAsync();
            using var reader = new StreamReader(stream);
            var resp = await reader.ReadToEndAsync();

            if (response.IsSuccessStatusCode)
            {
                var flightPlanId = Regex.Replace(resp, @"[^\d]", "");
                return Convert.ToInt32(flightPlanId);
            }
            else
            {
                throw new Exception($"Error while posting the flight plan.\nStatus code: {response.StatusCode}\nError: {resp}");
            }
        }

        private void InitRestClient(string url, string token = "")
        {
            _efbApi = RestService.For<IEfbApi>(
                new HttpClient(new AuthenticatedHttpClientHandler(token))
                {
                    BaseAddress = new Uri(url),
                    Timeout = TimeSpan.FromSeconds(45)
                });
        }
    }
}
