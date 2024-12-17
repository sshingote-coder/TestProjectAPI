
using Microsoft.OpenApi.Models;
using TestProjectAPI.Services;
using TestProjectAPI.Store;

namespace TestProjectAPI
{
    public class Program
    {

        public static string CorsPolicyName = "CorsOriginPolicy";
        public static string SwaggerEndpointVersion = "v1";
        public static string SwaggerEndpointName = "POQ Coding Challenge API";
        public static string SwaggerEndpoint = $"/swagger/{SwaggerEndpointVersion}/swagger.json";
        public static string HealthEndpoint = "/health";
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

            // Add extra service for Diagnostic & Performance
            builder.Services.AddHealthChecks();
            builder.Services.AddLogging();
            builder.Services.AddMemoryCache();

            // Add basic Security & Regulation support
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(CorsPolicyName, builder =>
                {
                    builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            });

            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(SwaggerEndpointVersion,
                    new OpenApiInfo { Title = SwaggerEndpointName, Version = SwaggerEndpointVersion });
            });

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddSingleton<IProductStore, ProductStore>();
            builder.Services.AddSingleton<IProductService, ProductService>();
            var app = builder.Build();


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

                app.UseHttpsRedirection();

                app.UseCors(CorsPolicyName);

                app.UseRouting();

                app.UseAuthorization();

                // Enable middleware to serve generated Swagger as a JSON endpoint.
                app.UseSwagger();
                // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint(SwaggerEndpoint, SwaggerEndpointName);
                    c.RoutePrefix = string.Empty;
                });

                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                    endpoints.MapHealthChecks(HealthEndpoint);
                    endpoints.MapSwagger();
                });


            app.Run();
        }
    }
}
