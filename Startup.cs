using ASPNETCoreMVCwithReact.Codes;
using Forge.Logging;
using Forge.Net.WebSockets;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
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

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });

#if DEBUG
            // depends on what you need, you can use the WebSocket feature. If you use, just remove the preprocessor.
            services.AddWebSocketManager();
#endif
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
            app.UseExceptionHandler("/Home/Error");

            if (!env.IsDevelopment())
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();

#if DEBUG
            // depends on what you need, you can use the WebSocket feature. In that case, mode the DEBUG check preprocessor after 'app.UseWebSocket();'
            var serviceScopeFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();
            var serviceProvider = serviceScopeFactory.CreateScope().ServiceProvider;
            app.UseWebSockets();
            app.UseWebSocketManager("/browserrefresh", serviceProvider.GetService<BrowserRefresh>());
#endif

            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

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

            app.UseSpa(spa =>
            {
                if (env.IsDevelopment())
                {
                    spa.Options.SourcePath = "ClientApp";
                }
                else
                {
                    spa.Options.SourcePath = "ClientApp/build";
                }

                //if (env.IsDevelopment())
                //{
                //    spa.UseReactDevelopmentServer(npmScript: "start");
                //}
            });

        }
    }
}
