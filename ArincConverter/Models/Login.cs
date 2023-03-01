namespace ArincConverter.Models
{
    public class LoginRequest
    {
        public LoginRequest(string username, string icao, string password, bool isActiveDirectoryUser, string role, string version)
        {
            Username = username;
            ICAO = icao;
            Password = password;
            IsActiveDirectoryUser = isActiveDirectoryUser;
            Role = role;
            Version = version;
        }

        public string Username { get; set; }
        public string ICAO { get; set; }
        public string Password { get; set; }
        public bool IsActiveDirectoryUser { get; set; }
        public string Role { get; set; }
        public string Version { get; set; }
    }
    
    public class LoginResponse
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}
