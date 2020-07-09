using BaseCleanArchitecture.API.Filters;
using BaseCleanArchitecture.Core.Interfaces;
using BaseCleanArchitecture.Infra.Logs;
using BaseCleanArchitecture.Core.Shared.Interfaces;
using BaseCleanArchitecture.Infra.Data;
using BaseCleanArchitecture.UseCases;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace BaseCleanArchitecture.API
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
            var strConn = System.Environment.GetEnvironmentVariable("AppConnection") ?? Configuration.GetConnectionString("AppConnection");
            services.AddDbContext<AppContext>(options => options.UseSqlServer(strConn));

            services.AddScoped<IRepository, AppRepository>();
            services.AddSingleton<IScopeInformation, ScopeInformation>();
            services.AddScoped<IUser, UserUseCase>();

            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Base Clean Architecture API", Version = "v1" });
            });

            services.AddHealthChecks()
                .AddSqlServer(strConn, name: "SQL Server", failureStatus: HealthStatus.Unhealthy,
                    tags: new string[] {"Database Connection"})
                .AddDbContextCheck<AppContext>(name:"DB Context", HealthStatus.Unhealthy,
                    new string[] {"Database Connection"});
            services.AddHealthChecksUI();
            
            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(TrackPerformanceFilter));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("./swagger/v1/swagger.json", "Base Clean Architecture API V1");
                c.RoutePrefix = string.Empty;
            });
            
            app.UseHealthChecks("/health", new HealthCheckOptions()
            {
                Predicate = _ => true,
                ResultStatusCodes =
                {
                    [HealthStatus.Healthy] = StatusCodes.Status200OK,
                    [HealthStatus.Degraded] = StatusCodes.Status500InternalServerError,
                    [HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable
                },
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
                AllowCachingResponses = false
            });
            app.UseHealthChecksUI(config=> config.UIPath = "/health-ui");
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
