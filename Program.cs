using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using pos_koperasi.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<RazorPagesBarangContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("RazorPagesBarangContext") ?? throw new InvalidOperationException("Connection string 'RazorPagesBarangContext' not found.")));

// Add authentication services
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Login";
        options.LogoutPath = "/Login?handler=Logout";
    });

// Add authorization services
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("CanManageBarang", policy =>
        policy.RequireRole("Admin"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
