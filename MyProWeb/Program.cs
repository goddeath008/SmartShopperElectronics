using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;

using MyProWeb.Areas.Customer.Repository;
using MyProWeb.Data;
using MyProWeb.Models.Domain;
using System;


namespace MyProWeb
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // Add DbContexts
            builder.Services.AddDbContext<ThaimcqlGodContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ThaimcqlGodContext")));
            builder.Services.AddDbContext<AuthenDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("AuthenDbContext")));

            // Add Repository
            builder.Services.AddScoped<IDanhMucSPRepository, DanhMucSPRepository>();

            // Register Identity
            builder.Services.AddIdentity<AppUser, IdentityRole>()
                .AddEntityFrameworkStores<AuthenDbContext>()
                .AddDefaultTokenProviders();
            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/login/";
                options.LogoutPath = "/logout";
                options.AccessDeniedPath = "/CantAccess";
            });
            builder.Services.AddAuthorization(options => {
                options.AddPolicy("Manager", policyBuilder => {
                    policyBuilder.RequireAuthenticatedUser();
                    policyBuilder.RequireRole("Manager");
                });
                options.AddPolicy("ThaiMC", policyBuilder => {
                    policyBuilder.RequireAuthenticatedUser();
                    policyBuilder.RequireRole("Manager");
                    policyBuilder.RequireRole("Admin");
                    policyBuilder.RequireRole("Editor");
                    policyBuilder.RequireRole("ThaiMC");
                });
                options.AddPolicy("Admin", policyBuilder => {
                    policyBuilder.RequireAuthenticatedUser();
                    policyBuilder.RequireRole("Admin");
                    policyBuilder.RequireRole("Editor");
                });
                options.AddPolicy("Editor", policyBuilder => {
                    policyBuilder.RequireAuthenticatedUser();
                    policyBuilder.RequireRole("Editor");
                    policyBuilder.RequireRole("Admin");
                });
            });




            builder.Services.AddOptions();
            var mailSettings = builder.Configuration.GetSection("MailSettings");
            builder.Services.Configure<MailSettings>(mailSettings);
            builder.Services.AddSingleton<IEmailSender, SendMailService>();



            // Add Razor Pages
            builder.Services.AddRazorPages();
            builder.Services.AddSession();
            //add google config
            builder.Services.AddAuthentication()
                .AddGoogle(options =>
                {
                    var gg = builder.Configuration.GetSection("Authentication:Google");
                    options.ClientId = gg["ClientId"];
                    options.ClientSecret = gg["ClientSecret"];
                    options.CallbackPath = "/loginGoogle";
                })
                .AddFacebook(facebookOptions =>
                {
                    // Đọc cấu hình
                    var fb = builder.Configuration.GetSection("Authentication:Facebook");
                    facebookOptions.AppId = fb["AppId"];
                    facebookOptions.AppSecret = fb["AppSecret"];
                    // Thiết lập đường dẫn Facebook chuyển hướng đến
                    facebookOptions.CallbackPath = "/loginFacebook";
                });

                    // Configure Identity Options
                    builder.Services.Configure<IdentityOptions>(options =>
            {
                // Password settings
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 3;
                options.Password.RequiredUniqueChars = 1;

                // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true;

                // SignIn settings
                options.SignIn.RequireConfirmedEmail = true;
                options.SignIn.RequireConfirmedPhoneNumber = false;
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseSession();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}");

            app.MapRazorPages();

            app.Run();
        }
    }
}
