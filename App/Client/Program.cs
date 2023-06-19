using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using TimetablePlanning.App.Client;
using TimetablePlanning.Components.Scheduling.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");

builder.Services.AddScoped(_ => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
builder.Services.AddScoped<ITimetableService, ExampleTimetableService>();
builder.Services.AddLocalization(options =>
{
    options.ResourcesPath = "Resources";

});

var app = builder.Build();


await app.RunAsync();

