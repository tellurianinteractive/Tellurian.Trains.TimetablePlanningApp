using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Tellurian.Trains.Scheduling.Services;

#pragma warning disable RCS1090 // Add call to 'ConfigureAwait' (or vice versa).
#pragma warning disable RCS1102 // Make class static.

namespace TimetablePlanning.App.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.Services.AddScoped(_ => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
            builder.Services.AddScoped<ITimetableService, ExampleTimetableService>();
            await builder.Build().RunAsync();
        }
    }
}
