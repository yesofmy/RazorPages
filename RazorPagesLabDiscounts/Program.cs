var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages(options =>
{
    options.Conventions.ConfigureFilter(
        new Microsoft.AspNetCore.Mvc.IgnoreAntiforgeryTokenAttribute()
    );
});

var app = builder.Build();

app.UseStaticFiles();

app.MapRazorPages();

app.Run();