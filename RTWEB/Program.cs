using Microsoft.EntityFrameworkCore;
using RTWEB.Data;
using RTWEB.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<Db>(options => options.UseSqlServer(Db.ConnectionString));
builder.Services.AddScoped<IUnitofWork, UnitOfWork>();

// 🔹 Add distributed memory cache (required for session)
builder.Services.AddDistributedMemoryCache();

// 🔹 Add session service
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // session expire after 30 minutes
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

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

// 🔹 Use session middleware before authorization
app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();
