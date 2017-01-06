using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Wesley.Lottery.Core.Filters;
using Wesley.Lottery.Core.Settings;
using Wesley.Lottery.Core.Helpers;

namespace Wesley.Lottery.Website
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            //启用MVC
            services.AddMvc(options => {
                options.Filters.Add(typeof(AppFilter));
            });

            //启用Session
            services.AddSession(options => {
                var cookieName = Configuration["AppSettings:Session:CookieName"];
                var timeout = Convert.ToInt32(Configuration["AppSettings:Session:Timeout"]);
                options.CookieName = cookieName;
                options.IdleTimeout = new TimeSpan(0, timeout, 0);
            });

            //启用分布式缓存
            services.AddDistributedRedisCache(options =>
            {
                options.Configuration = Configuration["AppSettings:Caching:ConnectionString"];
            });

            //启用配置
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

            //启用操作
            services.AddScoped<IHttpContextAccessor, HttpContextAccessor>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            //使用静态文件
            app.UseStaticFiles();

            //使用Session
            app.UseSession();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            //配置Helper
            SessionHelper.Configure(app.ApplicationServices.GetRequiredService<IHttpContextAccessor>());

            
        }
    }
}
