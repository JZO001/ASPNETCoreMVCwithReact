using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Forge.Logging;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ASPNETCoreMVCwithReact
{
    public class Startup
    {

        private static ILog LOGGER = null;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            // initialize logger
            loggerFactory.AddLog4Net();
            Forge.Logging.LogManager.LOGGER = Forge.Logging.Log4net.Log4NetManager.Instance;
            Forge.Logging.LogUtils.LogAll();
            LOGGER = LogManager.GetLogger(typeof(Startup));

            // handling error before IIS catch them
            app.UseStatusCodePagesWithReExecute("/Home/Errors/{0}");


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
            app.UseStaticFiles("/wwwroot");

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                // default map (landing page)
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                // this handles the unknown initial reuqest
                endpoints.MapControllerRoute(
                    name: "errors",
                    pattern: "Home/Errors/{id?}",
                    defaults: new { controller = "Home", action = "Errors" });

                // add your endpoints to handle initial requests
                endpoints.MapControllerRoute(
                    name: "article",
                    pattern: "Article/{id?}",
                    defaults: new { controller = "Article", action = "Index" });

            });
        }
    }
}
