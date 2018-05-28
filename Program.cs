using Microsoft.Extensions.Configuration;
using System.Threading;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Extensibility;
using System;
using System.Threading.Tasks;

namespace SimulatedDevice
{
    class Program
    {
        static TelemetryClient client; 

        static async Task<int> Main(string[] args)
        {
            // Adding all environment variables into IConfiguration
            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true) //Only used for dev environment
                .AddJsonFile("/secrets/IoTHubCredentials.json", optional: true) //Only present when in kubernetes
                .AddEnvironmentVariables() //Container Parameters
                .Build();

            Console.WriteLine($"Pod Name is: {config["POD_NAME"]}");
            Console.WriteLine($"Application Insights Key: {config["APPINSIGHTS_INSTRUMENTATIONKEY"]}");
            Console.WriteLine($"Image delay is: {config["ImageDelay"]}");
            Console.WriteLine($"Readings delay is: {config["ReadingsDelay"]}");
            string devicekeyPath = $"IoTHubCreds:{config["POD_NAME"]}"; 
            Console.WriteLine($"IoT Credentials are: {config[devicekeyPath]}");
            Thread.Sleep(Timeout.Infinite);
            return 0;
        }
    }
}
