using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PechinchaMarket.Areas.Identity.Data;
using PechinchaMarket.Models;
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DBPechinchaMarketContextConnection") ?? throw new InvalidOperationException("Connection string 'DBPechinchaMarketContextConnection' not found.");

builder.Services.AddDbContext<DBPechinchaMarketContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<PechinchaMarketUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<DBPechinchaMarketContext>();




// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();
app.Run();
