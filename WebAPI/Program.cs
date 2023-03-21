using BlockRouter.WebAPI.Controllers.Repositories.Base;
using BlockRouter.WebAPI.Models.Entities;
using Serilog;
using WebAPI.Controllers.Repositories;
using WebAPI.Controllers.Repositories.Base;

namespace BlockRouter.WebAPI
{
#pragma warning disable S1118 // Utility classes should not have public constructors (Reference to class in DataContext configuration).
    public class Program
    {
        public static void Main(string[] args)
        {
            ConfigureEnvironment();

            var builder = WebApplication.CreateBuilder(args);
            ConfigureBuilder(builder);

            var app = builder.Build();
            ConfigureApplication(app);

            app.Run();
        }

        private static void ConfigureEnvironment()
        {
            Log.Logger = new LoggerConfiguration().WriteTo
                                                  .Console()
                                                  .CreateLogger();
        }

        private static void ConfigureBuilder(WebApplicationBuilder builder)
        {

            builder.Host.UseSerilog();

            ConfigureServices(builder.Services);
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddDbContext<PostgresDataContext>();

            services.AddSwaggerGen();
            services.AddEndpointsApiExplorer();

            services.AddScoped<IBrandRepository, BrandRepository>();
            services.AddScoped<IModelRepository, ModelRepository>();

            Log.Information("Services initialized successfully.");
        }

        private static void ConfigureApplication(WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();

            app.MapControllers();

            Log.Information("Application is configured successfully.");
        }
    }
}
