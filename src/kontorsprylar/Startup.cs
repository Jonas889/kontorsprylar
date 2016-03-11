using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Data.Entity;
using kontorsprylar.Models;

namespace kontorsprylar
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            var connString = @"Server=tcp:emperor.database.windows.net,1433;Database=KontorsPrylar_DB;User ID=root_emperor@emperor;Password=Academy16;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

            services.AddEntityFramework()
                .AddSqlServer()
                .AddDbContext<StoredDbContext>(options =>
                  options.UseSqlServer(connString));

            services.AddMvc();
            services.AddCaching(); 
            services.AddSession();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            app.UseSession();
            app.UseDeveloperExceptionPage();
            app.UseMvcWithDefaultRoute();
            app.UseStaticFiles();
        }

        // Entry point for the application.
        public static void Main(string[] args) => WebApplication.Run<Startup>(args);
    }
}
