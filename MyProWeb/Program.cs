using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyProWeb.Data;
using MyProWeb.Models.Domain;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
//add dbcontext
builder.Services.AddDbContext<ThaimcqlGodContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ThaimcqlGodContext")));
builder.Services.AddDbContext<AuthenDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("AuthenDbContext")));
//Add Identity DB
builder.Services.AddIdentity<UserAD, IdentityRole>()
    .AddEntityFrameworkStores<AuthenDbContext>();

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}");


app.Run();
