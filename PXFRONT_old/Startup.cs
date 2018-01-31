using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using static PXFRONT.appsettingsCL;

namespace PXFRONT
{
    public class Startup
    {
        public IConfiguration Configuration { get; private set; }

        public Startup(IHostingEnvironment env)
        {
            // appsettings.jsonファイルの読み込み
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.Configure<PXAS_AppSetCL>(Configuration.GetSection("PXAS_AppSetCL"));
            services.Configure<Logging>(Configuration.GetSection("Logging"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Start/Error");
            }

            //ブール値のロード
            string isDemoMode = this.Configuration.GetValue<string>("PXAS_AppSetCL:Hoge");
            //文字列値のロードは、インデクサで指定可能
            string defaultUserName = this.Configuration["PXAS_AppSetCL:HogeHoge"];

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                  name: "areaRoute",
                  template: "{area}/{controller=Start}/{action=Assortment}/{id?}");
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Start}/{action=Assortment}/{id?}");
            });
        }
    }
}
