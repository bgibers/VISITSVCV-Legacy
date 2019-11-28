using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using visitsvc.DataAccess;
using visitsvc.Models;

namespace visitsvc
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
            services.AddControllers();
            
            services.AddDbContext<visitContext>( 
                options => options.UseMySql("Server=localhost; " +
                                            "Port=1521; " +
                                            "Database=visit; " +
                                            "User=root; " +
                                            "Password=clemson17;", 
                    mySqlOptions =>
                    {
                        mySqlOptions.ServerVersion(new Version(5, 7, 17), ServerType.MySql); // replace with your Server Version and Type
                    }
                ));
            
            services.AddSwaggerDocument();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Visit API",
                    Description = "The offical Web API for Visit backend",
                    TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Brendan Giberson",
                        Email = string.Empty,
                        Url = new Uri("http://visittravelapp.com"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Use under LICX",
                        Url = new Uri("https://example.com/license"),
                    }
                });
                
                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            app.UseOpenApi();
            app.UseSwaggerUi3();
            app.UseHttpsRedirection();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Visit API V1");
                c.RoutePrefix = string.Empty;
            });
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
            
        }
    }
}