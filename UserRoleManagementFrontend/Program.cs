

using Microsoft.AspNetCore.Authentication.Cookies;
using UserManagement.TableCreation;
using UserRoleManagementFrontend.Infrastructure;
using UserRoleManagementFrontend.Services;

namespace UserRoleManagementFrontend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
        
            
            builder.Services
                .Configure<ApiConfigurations>(builder.Configuration.GetSection("ApiConfigurations"))
                .AddHttpContextAccessor()
                .AddScoped<IAuthenticateService,AuthenticationService>()
                .AddScoped<IRepositoryAsync<Role>, RolesApiRepository>()
                .AddScoped<IRepositoryAsync<User>,UsersApiRepository>()
                .AddSession(config =>
                {
                    config.IdleTimeout = TimeSpan.FromMinutes(30);
                    config.Cookie.Name = "ASPNET_Session";
                    config.Cookie.HttpOnly = true;

                });

            builder.Services.AddAuthentication(
    CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
    {
        options.Cookie.HttpOnly = true;
        //options.ExpireTimeSpan
        options.LoginPath = "/accounts/Login";//LoginPath = All un-Authenticated users will be redirected to this
        options.LogoutPath = "/accouts/Logout";
        //options.Cookie.Expiration =
    });


            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
            app.UseSession();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Accounts}/{action=Login}/{id?}");

            app.Run();
        }
    }
}