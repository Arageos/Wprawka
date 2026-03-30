using Microsoft.EntityFrameworkCore;
using GamePlatform.Data;

var builder = WebApplication.CreateBuilder(args);

// Zamiana Razor Pages na MVC
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<GamePlatformContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("GamePlatform")));

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();