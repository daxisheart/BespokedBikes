using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BespokedBikes.Data;
using Microsoft.EntityFrameworkCore;
using BespokedBikes.Services;

namespace BespokedBikes
{
    public class Startup
    {
        private static string ConnectionString = "Server=tcp:solomon-profisee-app.database.windows.net,1433;Initial Catalog=BeSpokedBikesDB;Persist Security Info=False;" +
    "User ID=solomon;Password=Pr0f!s33;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.AddDbContext<BespokedBikesContext>(options =>
        options.UseSqlServer("Server=tcp:BespokedBike.database.windows.net,1433;Database=BikesDB;User ID=gaiaflare;Password=Pentras2;Encrypt=true;Connection Timeout=30"));
                //options.UseSqlServer(Configuration.GetConnectionString("BespokedBikesContext")));
            services.AddScoped<BespokedBikeService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

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
