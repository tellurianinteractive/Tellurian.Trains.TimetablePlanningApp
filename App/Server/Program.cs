using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TimetablePlanning.App.Server.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
if (builder.Environment.IsDevelopment()) builder.Configuration.AddUserSecrets<Program>();
builder.Services.AddDbContextFactory<TimetablesDbContext>(options =>
{
	options.UseSqlServer(builder.Configuration.GetConnectionString("TimetablePlanningDatabase")).EnableSensitiveDataLogging(builder.Environment.IsDevelopment());
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseWebAssemblyDebugging();
}
else
{
	app.UseExceptionHandler("/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();


app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
