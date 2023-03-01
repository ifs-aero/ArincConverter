using ArincConverter.Contracts;
using ArincConverter.Helpers;
using ArincConverter.Models;
using Refit;
using System.IdentityModel.Tokens.Jwt;
using System.Text.RegularExpressions;

namespace ArincConverter.Services
{
    public class EfbApiService : IEfbApiService
    {
        private IEfbApi _efbApi;

        private async Task<LoginResponse> Login(LoginRequest request, string environment, CancellationToken token)
        {
            InitRestClient(environment);
            return await _efbApi.Login(request, token);
        }

        public async Task<int> PostFlightPlan(LoginRequest request, FlightPlan flightPlan, string environment)
        {
            Console.WriteLine("\n\nAuthenticating...");
            var loginResponse = await Login(request, environment, CancellationToken.None);

            InitRestClient(environment, loginResponse.Token);

            Console.WriteLine("\nPosting flight plan...");
            flightPlan.AirlineId = GetAirlineId(loginResponse.Token);
            var response = await _efbApi.PostFlightPlan(new[] { flightPlan });
            if (response.IsSuccessStatusCode)
            {
                await using var stream = await response.Content.ReadAsStreamAsync();
                using var reader = new StreamReader(stream);
                var resp = await reader.ReadToEndAsync();
                var flightPlanId = Regex.Replace(resp, @"[^\d]", "");
                return Convert.ToInt32(flightPlanId);
            }
            else
            {
                throw new Exception($"Error while posting the flight plan. Status code: {response.StatusCode}");
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

        private int GetAirlineId(string token)
        {
            var jwtHandler = new JwtSecurityTokenHandler();
            var jwt = jwtHandler.ReadJwtToken(token);
            return Convert.ToInt32(jwt.Claims.FirstOrDefault(c => c.Type == "AirlineId")?.Value);
        }
    }
}
