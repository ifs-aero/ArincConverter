using ArincConverter.Models;
using Refit;

namespace ArincConverter.Contracts
{
    public interface IEfbApi
    {
        [Post("/auth/login")]
        Task<LoginResponse> Login([Body] LoginRequest request, CancellationToken token);

        [Post("/flights/flight-plan")]
        Task<HttpResponseMessage> PostFlightPlan(FlightPlan[] plans);
    }
}
