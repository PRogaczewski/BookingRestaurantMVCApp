using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RestaurantLibrary.Connection;
using RestaurantLibrary.Services;
using RestaurantBookingMVC.Middleware;
using Microsoft.AspNetCore.Identity;
using RestaurantLibrary.Models;

namespace RestaurantBookingMVC
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddAuthentication("CookieAuth").AddCookie("CookieAuth", options =>
            {
                options.Cookie.Name = "CookieAuth";
                options.LoginPath = "/Account/Login";
            });

            services.AddAuthorization(opions =>
            {
                opions.AddPolicy("AdminOnly", policy => policy.RequireRole(ApplicationRoles.Admin));
            });
            services.AddDbContext<RestaurantContext>(options => options.UseSqlServer(Configuration.GetConnectionString("RestaurantDbConnection")));
            services.AddDbContext<UsersDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("UserDbConnection")));
            services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<UsersDbContext>();
            services.AddScoped<IRestaurantService, RestaurantService>();
            services.AddScoped<ITablesService, TablesService>();
            services.AddScoped<ITimeService, TimeService>();
            services.AddScoped<ErrorHandlingMiddleware>();
            services.AddScoped<DbRolesInitializer>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, DbRolesInitializer initializer)
        {
            initializer.AddRoles();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseMiddleware<ErrorHandlingMiddleware>();

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
