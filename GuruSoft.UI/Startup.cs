using GuruSoft.Business;
using GuruSoft.Business.Interface;
using GuruSoft.Data.Context;
using GuruSoft.Data.Interface;
using GuruSoft.Data.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuruSoft.UI
{
    public class Startup
    {
        #region Member
        public IConfiguration Configuration { get; }
        #endregion

        #region Ctor
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        #endregion

        #region Methods
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Connection string to Data Base 
            string connString = Configuration.GetConnectionString("GURU");
            services.AddControllersWithViews();
            //Add Context 
            services.AddDbContext<GuruContext>(item => item.UseSqlServer(connString), ServiceLifetime.Transient);
            //mapper 
            LoadScopes(services);
            services.AddMvc();
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
        #endregion


        #region Private Methods 
        private void LoadScopes(IServiceCollection services)
        {
            //Repository
            services.AddScoped(typeof(IDefaultRepository<>), typeof(DefaultRepository<>));
            //Service
            services.AddScoped<IProductBusiness, ProductBusiness>();
        }
        #endregion
    }
}
