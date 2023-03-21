using ArincConverter.Contracts;
using ArincConverter.Helpers;
using ArincConverter.Models;
using ArincConverter.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.IdentityModel.Tokens.Jwt;
using System.Security;

using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddSingleton<IEfbApiService, EfbApiService>();
        services.AddSingleton<IPdfService, PdfService>();
        services.AddSingleton<IArincService, ArincService>();
    })
    .Build();

await RunServiceAsync(host.Services);

await host.RunAsync();

static async Task RunServiceAsync(IServiceProvider hostProvider)
{
    try
    {
        Console.WriteLine("\nSelect the environment:\n1) Development\n2) Test\n");
        var service = Console.ReadLine();
        if (service.IsNotNullOrEmpty())
        {
            var environment = service.ToLower() switch
            {
                "1" or "development" or "dev" => "https://api-dev.ifs.aero/api",
                "2" or "test" => "https://api-test.ifs.aero/api",
                _ => throw new Exception("Invalid environment"),
            };

            IEfbApiService efb = GetService<IEfbApiService>(hostProvider);

            var token = await efb.Login(BuildLoginRequest(), environment);

            FlightPlan? flightPlan = ConvertArincFile(hostProvider, GetUser(token));
            if (flightPlan != null)
            {
                await PushFlight(hostProvider, efb, flightPlan, token, environment);
            }
            else
            {
                Console.WriteLine("\nAn error occurred while converting the files to a flight plan.");
            }
        }
        else
        {
            throw new Exception("Invalid environment");
        }

        Console.WriteLine("\nPress any key to exit.");
        Console.ReadKey();
        Environment.Exit(0);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"\n{ex.Message}");
    }
}

static FlightPlan ConvertArincFile(IServiceProvider hostProvider, User user)
{
    Console.WriteLine("\nIntroduce the path of the Arinc xml files:");
    var filePath = Console.ReadLine();

    if (filePath?.IsValidPath() == true)
    {
        IArincService arinc = GetService<IArincService>(hostProvider);

        Console.WriteLine("\nConverting...");
        return arinc.GetFlightPlan(File.ReadAllBytes(filePath), user);
    }
    else
    {
        throw new Exception("The path is not valid.");
    }
}

static async Task PushFlight(IServiceProvider hostProvider, IEfbApiService efb, FlightPlan flightPlan, string token, string environment)
{
    var flightPlanId = await efb.PostFlightPlan(flightPlan, token, environment);

    Console.WriteLine($"\nFlight plan was created successfully...\nId: {flightPlanId}\nFlightPlanId: {flightPlan.ThirdPartyPlanId}\nFlightScheduledId: {flightPlan.ThirdPartyScheduleId}\n\n");
}

static LoginRequest BuildLoginRequest()
{
    Console.WriteLine("\nIntroduce the ICAO code:");
    var icao = Console.ReadLine();
    if (icao.IsNullOrEmpty())
    {
        throw new Exception("Invalid ICAO code");
    }

    Console.WriteLine("\nIntroduce your username:");
    var username = Console.ReadLine();
    if (username.IsNullOrEmpty())
    {
        throw new Exception("Invalid username or password");
    }

    Console.WriteLine("\nIntroduce your password:");
    var password = GetPassword();
    if (password.IsNullOrEmpty())
    {
        throw new Exception("Invalid username or password");
    }

    return new LoginRequest(username, icao, password, false, null, null);
}

static T GetService<T>(IServiceProvider hostProvider) where T : notnull
{
    using IServiceScope serviceScope = hostProvider.CreateScope();
    IServiceProvider provider = serviceScope.ServiceProvider;

    return (T)provider.GetRequiredService(typeof(T));
}

static string GetPassword()
{
    var pwd = new SecureString();
    while (true)
    {
        ConsoleKeyInfo i = Console.ReadKey(true);
        if (i.Key == ConsoleKey.Enter)
        {
            break;
        }
        else if (i.Key == ConsoleKey.Backspace)
        {
            if (pwd.Length > 0)
            {
                pwd.RemoveAt(pwd.Length - 1);
                Console.Write("\b \b");
            }
        }
        else if (i.KeyChar != '\u0000')
        {
            pwd.AppendChar(i.KeyChar);
            Console.Write("*");
        }
    }

    return new System.Net.NetworkCredential(string.Empty, pwd).Password;
}

static User GetUser(string jwt)
{
    var jwtHandler = new JwtSecurityTokenHandler();
    var token = jwtHandler.ReadJwtToken(jwt);

    var username = token.Claims.FirstOrDefault(c => c.Type == "unique_name")?.Value;
    var airline = Convert.ToInt32(token.Claims.FirstOrDefault(c => c.Type == "AirlineId")?.Value);
    var airlineName = token.Claims.FirstOrDefault(c => c.Type == "airlineName")?.Value;

    return new User(airline, airlineName, username);
}
