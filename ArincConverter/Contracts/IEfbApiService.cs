using ArincConverter.Models;

namespace ArincConverter.Contracts
{
    public interface IEfbApiService
    {
        Task<int> PostFlightPlan(LoginRequest request, FlightPlan flightPlan, string environment);
    }
}
