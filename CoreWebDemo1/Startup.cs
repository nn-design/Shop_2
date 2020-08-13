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
            //����ע��
            services.AddControllersWithViews();//ע��mvc����

            //ע��һ��ICarService����ICarService��CarServiceImplӳ���ϵע�ᵽIOC������

            //Transient(ÿ��ע�붼�ᴴ���µ�ʵ��)
            services.AddTransient<ICarService, CarServiceImpl>();

            //Scopedһ��(������ֻ����һ��ʵ��)
            //services.AddScoped<ICarService, CarServiceImpl>();

            //Singleton(��Ӧ�ó����������������ֻ�ᴴ��һ��)
            //services.AddSingleton<ICarService, CarServiceImpl>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //�����м��
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //�Զ����м��
           // app.Use( async(context, next) =>
           //{
           //    await context.Response.WriteAsync("Hello World!");
           //    await next();//ִ����һ���м��

           //});

            //����·���м��
            app.UseRouting();

            //�����ս���м������Ҫ��������·��
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
