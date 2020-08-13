using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreWebDemo1.Service;
using CoreWebDemo1.ServiceImpl;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CoreWebDemo1
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //依赖注入
            services.AddControllersWithViews();//注册mvc服务

            //注册一个ICarService，将ICarService和CarServiceImpl映射关系注册到IOC容器中

            //Transient(每次注入都会创建新的实例)
            services.AddTransient<ICarService, CarServiceImpl>();

            //Scoped一个(请求内只创建一个实例)
            //services.AddScoped<ICarService, CarServiceImpl>();

            //Singleton(在应用程序的整个生命周期只会创建一次)
            //services.AddSingleton<ICarService, CarServiceImpl>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //配置中间件
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //自定义中间件
           // app.Use( async(context, next) =>
           //{
           //    await context.Response.WriteAsync("Hello World!");
           //    await next();//执行下一个中间件

           //});

            //开启路由中间件
            app.UseRouting();

            //开启终结的中间件，主要用来定义路由
            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapGet("/", async context =>
                //{
                //    await context.Response.WriteAsync(" nmz!");
                //});
                endpoints.MapControllerRoute(
                        name: "defalut",
                        pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
