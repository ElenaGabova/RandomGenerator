using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Inreface;
using Database.Interface;
using Entities;
using Database.Repository;
using Interface;
using Mapper;
using Serilog;
using Microsoft.EntityFrameworkCore;
using Database;
using Microsoft.Extensions.Configuration;

namespace RandomNumberProjectView
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddControllersWithViews();
            services.AddMemoryCache();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IRandomCounterRepository, NumberCounterRepository>();
            services.AddSingleton<IConfiguration>(Configuration);

            services.AddTransient<IGenericRepository<NumberEntity>, EFGenericRepository<NumberEntity>>();
            services.AddTransient<IGenericRepository<NumberRepetitionEntity>, EFGenericRepository<NumberRepetitionEntity>>();
            services.AddTransient<IRandomGeneratorRepository, RandomGeneratorRepository>();
            services.AddTransient<INumberRepetitionRepository, NumberRepetitionRepository>();
            services.AddTransient<INumberRepository, NumberRepository>();
            services.AddAutoMapper(typeof(MappingProfile));
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
            }
            app.UseStaticFiles();

            app.UseRouting();

          //  app.UseSerilogRequestLogging();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                   name: "default",
                   pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
