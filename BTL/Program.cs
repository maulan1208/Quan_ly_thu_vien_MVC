using Microsoft.EntityFrameworkCore;
using BTL.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//Thiet lap dich vu ket noi toi Database
var connectionString = builder.Configuration.GetConnectionString("QLThuVienDB");
builder.Services.AddDbContext<QLThuVienDBContext>(opptions => opptions.UseSqlServer(connectionString));
builder.Services.AddControllersWithViews();

var app = builder.Build();
/*
// Khai bao su sung Session
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(60);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
*/

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//Khai bao su dung Session
//app.UseSession();
//

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
