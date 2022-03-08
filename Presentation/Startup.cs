using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using Microsoft.OpenApi.Models;
using Infrastructure.DbContext;
using Infrastructure.Repositories;
using System.Text.Json.Serialization;
using System.Reflection;
using System.IO;
using Application;

namespace Presentation
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
            var postgreSQLConnectionConfiguration = new PostgreConfiguration(Configuration.GetValue<string>("PostgreSQLConnection"));
            services.AddSingleton(postgreSQLConnectionConfiguration);
            
            services.AddScoped<InterfaceUsuarioRepository, UsuarioRepository>();
            services.AddScoped<InterfaceHotelRepository, HotelRepository>();
            services.AddScoped<InterfaceReservaRepository, ReservaRepository>();
            services.AddScoped<InterfaceUsuarioManager, UsuarioManager>();
            services.AddScoped<InterfaceHotelManager, HotelManager>();
            services.AddScoped<InterfaceReservaManager, ReservaManager>();
            
            services.AddControllers().AddJsonOptions(opts =>
            {
                opts.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                opts.JsonSerializerOptions.IgnoreNullValues = true;
            }); ;
            services.AddHealthChecks();
            AddSwagger(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseHealthChecks("/hc");
            app.UseHttpsRedirection();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Presentation");
            });
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void AddSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                var groupName = "v1";
                options.SwaggerDoc(groupName, new OpenApiInfo
                {
                    Title = $"Presentation",
                    Version = groupName,
                    Description = "Presentation API",
                });
                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
            });
        }
    }
}
