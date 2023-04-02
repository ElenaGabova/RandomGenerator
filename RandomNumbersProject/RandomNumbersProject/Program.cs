
using Database;
using Database.Interface;
using Database.Repository;
using Entities;
using Inreface;
using Interface;
using JavaScriptEngineSwitcher.ChakraCore;
using JavaScriptEngineSwitcher.Extensions.MsDependencyInjection;
using Mapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using React.AspNet;
using Repository;
using Serilog;

public class Program_old
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddDbContext<DatabaseContext>(options => options.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=RandomNumbers;Trusted_Connection=false;User Id =sa;Password=K6y&2xS1qa!"));

        var services = builder.Services;
        services.AddMemoryCache();
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddSingleton<IRandomCounterRepository, NumberCounterRepository>();

        services.AddTransient<IGenericRepository<NumberEntity>, EFGenericRepository<NumberEntity>>();
        services.AddTransient<IGenericRepository<NumberRepetitionEntity>, EFGenericRepository<NumberRepetitionEntity>>();
        services.AddTransient<IRandomGeneratorRepository, RandomGeneratorRepository>();
        services.AddTransient<INumberRepetitionRepository, NumberRepetitionRepository>();
        services.AddTransient<INumberRepository, NumberRepository>();
        services.AddSwaggerGen();
        services.AddReact();
        services.AddJsEngineSwitcher(options => options.DefaultEngineName = ChakraCoreJsEngine.EngineName).AddChakraCore();
        services.AddControllers();
        services.AddAutoMapper(typeof(MappingProfile));
        services.AddSwaggerGen();
        builder.Host.UseSerilog((hostingContext, loggerConfiguration) =>
        {
            loggerConfiguration
                 .ReadFrom.Configuration(hostingContext.Configuration)
                 .Enrich.FromLogContext()
                 .Enrich.WithProperty("ApplicationName", "RandomGenrator web api");
        });
        var app = builder.Build();


        app.UseSerilogRequestLogging();
        app.UseDeveloperExceptionPage();
        app.UseReact(config => { });
        app.UseDefaultFiles();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1"));
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
        app.Run();
    }
}