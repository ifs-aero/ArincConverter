using ArincConverter.Models;

namespace ArincConverter.Contracts
{
    public interface IArincService
    {
        FlightPlan GetFlightPlan(byte[] flightPlanData, User user);
    }
}
