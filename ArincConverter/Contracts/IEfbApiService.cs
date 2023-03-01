using ArincConverter.Models;

namespace ArincConverter.Contracts
{
    public interface IEfbApiService
    {
        Task<string> Login(LoginRequest request, string environment);
        Task<int> PostFlightPlan(FlightPlan flightPlan, string token, string environment);
    }
}
