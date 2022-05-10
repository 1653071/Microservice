using AuthService1.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ModelClassLibrary.connection;
using ModelClassLibrary.impl;
using ModelClassLibrary.iterface;
using ModelClassLibrary.permission.permistionhandle;
using ModelClassLibrary.permission.services;
using ModelClassLibrary.permission.services.impl;
using SampleApi1.Models;
using SampleApi1.Services;
using SampleApi1.Services.impl;
using System;
using System.Collections.Generic;
using System.Text;

namespace SampleApi1
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
            services.AddTransient<IHashPass, HashPass>();
            services.AddTransient<IUser, UserImpl>();
            services.AddDbContext<DataContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("sqlite")));
            List<string> temp = new List<string>() { "manager" };
            
            services.AddScoped<IUserPermission, UserPermissionImpl>();
            services.AddScoped<IAuthorizationHandler, PermissionHandler>();

            services.AddCors();
            // Add API Versioning to the service container
            services.AddApiVersioning(config =>
            {
                // Specify the default API Version
                config.DefaultApiVersion = new ApiVersion(1, 0);
                // If the client hasn't specified the API version in the request, use the default API version number 
                config.AssumeDefaultVersionWhenUnspecified = true;
                // Advertise the API versions supported for the particular endpoint
                config.ReportApiVersions = true;
            });
            services.AddMemoryCache();  
            // Add Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "DotnetVN Demo API",
                    Version = "1.0",
                    Description = "DotnetVN Demo API",
                    Contact = new OpenApiContact
                    {
                        Name = "DotnetVN Demo API",
                        Email = "duylinh191@gmail.com",
                        Url = new Uri("http://dotnetvn.com"),
                    }
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "DotnetVN Demo API");
            });



            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.UseHttpsRedirection();
        }
    }
}
