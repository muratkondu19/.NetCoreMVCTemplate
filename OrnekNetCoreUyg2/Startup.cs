using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;

namespace OrnekNetCoreUyg2
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
            //Uygulamaya mvc servislerini dahil etme, controller ve view mimarisi devreye giriyor
            services.AddControllersWithViews();
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");


                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            //Gelen iste�in rotas� bu middleware sayesinde belirlenir
            app.UseRouting();

            app.UseAuthorization();

            //Endpoint-> yap�lan iste�in var�� noktas� / Url / istek adresi
            //Bu uygulamaya gelen isteklerin hangi rotalar e�li�inde/hangi formatta gelece�i buradan bildirilecek
            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapRazorPages();
                endpoints.MapDefaultControllerRoute();
                //MapDefaultControllerRoute default �emas� {controller=Home}/{action=index}/{id?}
                //controller gelen iste�i kar��lar, action da controller i�indeki iste�in kulland��� metoddur
                //bu uygulamaya yap�lacak olan istekler bu �emaya uygun olarak yap�lacakt�r.
                //{controller=Home}/{action=index}/{id?}, controller bo� geliyorsa Home, action bo� geliyorsa Index action tetiklenmektedir. Id nullable.
                //endpoint tan�mlamas�nda {} i�erisinde parametre tan�mlanabilmektedir. Bu parametreler herhangi bir isimde olabilirler.
                //{controller}/{action}/{murat} -> mimari controller ve indexin neye kar��l�k geldi�ini bilir. 3. parametre mimari taraf�ndan anlaml� de�ildir, biz de�erini kullanabiliriz.

            });
        }
    }
}
