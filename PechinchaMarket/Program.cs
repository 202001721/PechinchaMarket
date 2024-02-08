using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PechinchaMarket.Areas.Identity.Data;
using PechinchaMarket.Models;

using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
})
    .AddCookie()
    .AddGoogle(GoogleDefaults.AuthenticationScheme, options =>
    {
        options.ClientId = builder.Configuration.GetSection("GoogleKeys:ClientId").Value;
        options.ClientSecret = builder.Configuration.GetSection("GoogleKeys:ClientSecret").Value;
    });
var connectionString = builder.Configuration.GetConnectionString("DBPechinchaMarketContextConnection") ?? throw new InvalidOperationException("Connection string 'DBPechinchaMarketContextConnection' not found.");

builder.Services.AddDbContext<DBPechinchaMarketContext>(options => options.UseSqlServer(connectionString));

// builder.Services.AddDefaultIdentity<PechinchaMarketUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<DBPechinchaMarketContext>();

builder.Services.AddIdentity<PechinchaMarketUser, IdentityRole>(options =>
options.SignIn.RequireConfirmedAccount = true)
.AddEntityFrameworkStores<DBPechinchaMarketContext>()
.AddDefaultTokenProviders()
.AddDefaultUI();


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

using (var scope = app.Services.CreateScope())
{
    var userManager =
    scope.ServiceProvider.GetRequiredService<UserManager<PechinchaMarketUser>>();
    var roleManager =
    scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    await DataSeeder.SeedData(userManager, roleManager);
}
app.Run();
