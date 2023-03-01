namespace ArincConverter.Models
{
    public class User
    {
        public User(int airlineId, string airlineName, string username)
        {
            AirlineId = airlineId;
            Username = username;
            AirlineName = airlineName;
        }

        public int AirlineId { get; set; }
        public string AirlineName { get; set; }
        public string Username { get; set; }
    }
}
