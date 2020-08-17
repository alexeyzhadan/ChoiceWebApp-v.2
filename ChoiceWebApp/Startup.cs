using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using ChoiceWebApp.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Linq;
using ChoiceWebApp.Extensions;
using System.Collections.Generic;

namespace ChoiceWebApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<IdentityUser>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;

                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 6;

            }).AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("User", policyBuilder =>
                    policyBuilder.RequireAuthenticatedUser().RequireClaim("FullName"));

                options.AddPolicy("Admin", policyBuilder => 
                    policyBuilder.RequireAuthenticatedUser().RequireAssertion(context => 
                        !context.User.Claims.Any(c => c.Type == "FullName")));
            });

            services.AddAntiforgery(o => o.HeaderName = "XSRF-TOKEN");

            services.Configure<List<string>>(Configuration.GetSection("DefaultData:Groups"));
            services.AddGroups();

            services.AddControllersWithViews();
            services.AddRazorPages().AddNewtonsoftJson();
        }

        public void Configure(IApplicationBuilder app,
                                    IWebHostEnvironment env,
                                    UserManager<IdentityUser> userManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseTopSecret();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });

            DataInitializer.SetData(userManager);
        }
    }
}