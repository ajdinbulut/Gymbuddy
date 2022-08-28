using Gymbuddy.Infrastructure;
using GymBuddy.Infrastructure.Repository;
using GymBuddy.Infrastructure.Repository.IRepository;
using GymBuddy.Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using Microsoft.AspNetCore.Identity;
using GymBuddy.Infrastructure.Utilities;
using Gymbuddy.Utilities;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("GymDBConnection") ?? throw new InvalidOperationException("Connection string 'GymDBConnection' not found.");

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<UserManager>();
builder.Services.AddScoped<FileManager>();
builder.Services.AddScoped<PostManager>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSession();
builder.Services.AddDbContext<GymDB>(options => options.UseNpgsql(
"Host=localhost;Port=5432;Database=GymBuddy;Username=postgres;Password=postgres"   )) ;
var app = builder.Build();

    //........
    builder.Services.AddDistributedMemoryCache();





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
app.UseAuthentication();;


app.UseAuthorization();
app.UseSession();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Privacy}/{id?}");

app.Run();
