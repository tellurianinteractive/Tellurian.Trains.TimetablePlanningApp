using Microsoft.EntityFrameworkCore;
using TimetablePlanning.App.Server.Data;
using TimetablePlanning.App.Shared.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
if (builder.Environment.IsDevelopment()) builder.Configuration.AddUserSecrets<Program>();
builder.Services.AddDbContextFactory<TimetablesDbContext>(options =>
{
	options.UseSqlServer(builder.Configuration.GetConnectionString("TimetablePlanningDatabase"))
		.EnableSensitiveDataLogging(builder.Environment.IsDevelopment());
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

app.UseRequestLocalization(options =>
{
	options.SetDefaultCulture(LanguageService.DefaultLanguage);
	options.AddSupportedCultures(LanguageService.Languages);
	options.AddSupportedUICultures(LanguageService.Languages);
	options.FallBackToParentCultures = true;
	options.FallBackToParentUICultures = true;
});



app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();


app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
