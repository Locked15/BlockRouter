using BlockRouter.WebAPI.Controllers.Repositories;
using BlockRouter.WebAPI.Controllers.Repositories.Base;
using BlockRouter.WebAPI.Models.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Text;

namespace BlockRouter.WebAPI
{
#pragma warning disable S1118 // Utility classes should not have public constructors (Reference to class in DataContext configuration).
    public class Program
    {
        public const string JwtKeyName = "Jwt:Key";

        public const string JwtIssuerName = "Jwt:Issuer";

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

            ConfigureServices(builder.Services, builder.Configuration);
        }

        private static void ConfigureServices(IServiceCollection services, ConfigurationManager config)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CORS-Policy", GenerateCors());
            });
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options => options.TokenValidationParameters = GenerateTokenParameters(config));

            services.AddControllers();
            services.AddDbContext<PostgresDataContext>();

            // Add JWT support to Swagger.
            services.AddSwaggerGen(setup =>
            {
                setup.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Description = "JWT Token auth system to this API.",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                setup.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    // There is 'Dictionary<OpenApiSecurityScheme, List<string>>' value.
                    {
                        new OpenApiSecurityScheme()
                        {
                            Name = "Bearer",
                            Scheme = "oauth2",
                            In = ParameterLocation.Header,
                            Reference = new OpenApiReference()
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                        },
                        new List<string>()
                    }
                });
            });

            services.AddScoped<IRepository<Brand>, BrandRepository>();
            services.AddScoped<IRepository<Model>, ModelRepository>();

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
            app.MapControllers();

            app.UseCors("CORS-Policy");
            app.UseAuthentication();
            app.UseAuthorization();

            Log.Information("Application is configured successfully.");
        }

        #region Standalone Functions.

        private static CorsPolicy GenerateCors()
        {
            var corsBuilder = new CorsPolicyBuilder().AllowAnyHeader()
                                                     .AllowAnyMethod()
                                                     .AllowCredentials();
            return corsBuilder.Build();
        }

        private static TokenValidationParameters GenerateTokenParameters(ConfigurationManager config)
        {
            return new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,

                ValidIssuer = config.GetValue<string>(JwtIssuerName),
                ValidAudience = config.GetValue<string>(JwtIssuerName),
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.GetValue<string>(JwtKeyName)!))
            };
        }

        private static void SetupSwaggerDocs(SwaggerGenOptions setup)
        {
            setup.SwaggerDoc("BlockRouter-API Docs", new OpenApiInfo()
            {
                Title = "BlockRouter-API",
                License = new()
                {
                    Name = "MIT License",
                    Url = new("https://github.com/Locked15")
                }
            });
        }
        #endregion
    }
}
