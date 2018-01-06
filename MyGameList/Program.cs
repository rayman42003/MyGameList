using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using MyGameList.Data;
using Serilog;
using Serilog.Events;
using System;

namespace MyGameList
{
    public class Program
    {
        public static void Main(string[] args) {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .CreateLogger();

            var webHost = BuildWebHost(args);
            string environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            using (var scope = webHost.Services.CreateScope()) {
                var services = scope.ServiceProvider;
                if (environment == EnvironmentName.Development) {
                    try {
                        var gameContext = services.GetRequiredService<GameContext>();
                        DevDbInitalizer.Initalize(gameContext);
                    } catch (Exception e) {
                        Log.Error(e, $"An error occured in {environment} environment seeding the database.");
                    }
                }
            }

            webHost.Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseSerilog()
                .Build();
    }
}
