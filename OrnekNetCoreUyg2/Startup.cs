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

            //Gelen isteðin rotasý bu middleware sayesinde belirlenir
            app.UseRouting();

            app.UseAuthorization();

            //Endpoint-> yapýlan isteðin varýþ noktasý / Url / istek adresi
            //Bu uygulamaya gelen isteklerin hangi rotalar eþliðinde/hangi formatta geleceði buradan bildirilecek
            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapRazorPages();
                endpoints.MapDefaultControllerRoute();
                //MapDefaultControllerRoute default þemasý {controller=Home}/{action=index}/{id?}
                //controller gelen isteði karþýlar, action da controller içindeki isteðin kullandýðý metoddur
                //bu uygulamaya yapýlacak olan istekler bu þemaya uygun olarak yapýlacaktýr.
                //{controller=Home}/{action=index}/{id?}, controller boþ geliyorsa Home, action boþ geliyorsa Index action tetiklenmektedir. Id nullable.
                //endpoint tanýmlamasýnda {} içerisinde parametre tanýmlanabilmektedir. Bu parametreler herhangi bir isimde olabilirler.
                //{controller}/{action}/{murat} -> mimari controller ve indexin neye karþýlýk geldiðini bilir. 3. parametre mimari tarafýndan anlamlý deðildir, biz deðerini kullanabiliriz.

            });
        }
    }
}
