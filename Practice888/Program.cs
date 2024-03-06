using Microsoft.EntityFrameworkCore;
using Practice888.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<MasterDB>(x=>x.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=PDBB8;Trusted_Connection=True;")); 
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Sale}/{action=Index}/{id?}");

app.Run();
