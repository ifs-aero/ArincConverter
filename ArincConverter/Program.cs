using ArincConverter.Contracts;
using ArincConverter.Helpers;
using ArincConverter.Models;
using ArincConverter.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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
        FlightPlan? flightPlan = RunArinc(hostProvider);
        if (flightPlan != null)
        {
            await PushFlight(hostProvider, flightPlan);
        }
        else
        {
            Console.WriteLine("\nAn error occurred while converting the files to a flight plan.");
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

static FlightPlan RunArinc(IServiceProvider hostProvider)
{
    Console.WriteLine("\nIntroduce the path of the Arinc xml files:");
    var filePath = Console.ReadLine();

    if (filePath?.IsValidPath() == true)
    {
        IArincService arinc = GetService<IArincService>(hostProvider);

        Console.WriteLine("\nConverting...");
        return arinc.GetFlightPlan(File.ReadAllBytes(filePath));
    }
    else
    {
        throw new Exception("The path is not valid.");
    }
}

static async Task PushFlight(IServiceProvider hostProvider, FlightPlan flightPlan)
{
    Console.WriteLine("\nSelect the environment:\n1) Development\n2) Test\n");
    var service = Console.ReadLine();
    if (service.IsNotNullOrEmpty())
    {
        var environment = service.ToLower() switch
        {
            "1" or "development" => "https://api-dev.ifs.aero/api",
            "2" or "test" => "https://api-test.ifs.aero/api",
            _ => throw new Exception("Invalid environment"),
        };

        IEfbApiService efb = GetService<IEfbApiService>(hostProvider);
        
        var flightPlanId = await efb.PostFlightPlan(BuildLoginRequest(), flightPlan, environment);
        
        Console.WriteLine($"\nFlight plan was created successfully...\nId: {flightPlanId}\nFlightPlanId: {flightPlan.ThirdPartyPlanId}\nFlightScheduledId: {flightPlan.ThirdPartyScheduleId}\n\n");
    }
    else
    {
        throw new Exception("Invalid environment");
    }
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
