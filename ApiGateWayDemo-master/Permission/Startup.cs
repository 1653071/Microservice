using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using ModelClassLibrary.connection;
using ModelClassLibrary.models;
using ModelClassLibrary.permission.permistionhandle;
using ModelClassLibrary.permission.services.impl;
using Newtonsoft.Json;
using Permission.services;
using Permission.services.impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Permission
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
            AuthenticationConfig authenticationConfig = new AuthenticationConfig();
            Configuration.Bind("Authentication", authenticationConfig);
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(o =>
            {
                o.TokenValidationParameters = new TokenValidationParameters()
                {
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationConfig.AccessTokenSecret)),
                    ValidIssuer = authenticationConfig.Issuer,
                    ValidAudience = authenticationConfig.Audience,
                    ValidateIssuerSigningKey = true,
                    ValidateAudience = true,
                    ValidateIssuer = true,


                };
            });
            List<string> temp = new List<string>() {"admin"};
            services.AddAuthorization(options =>
            {
                
                
                    options.AddPolicy("admin",
                        policy => {
                            policy.AddRequirements(new PermissionRequirement(temp));
                            policy.RequireAuthenticatedUser();
                        });
                

            });

            services.AddControllers().AddNewtonsoftJson(options =>
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            services.AddScoped<ModelClassLibrary.permission.services.IUserPermission, UserPermissionImpl>();
            services.AddScoped<IAuthorizationHandler, PermissionHandler>();
            services.AddTransient<IUserPermission, UserPermission>();
            services.AddTransient<IPermission, PermissionImpl>();
            services.AddTransient<IGroup, GroupImpl>();
            services.AddTransient<IGroupPermission, GroupPermissionImpl>();
            services.AddDbContext<PermissionContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("sqlite")));
            services.AddCors();
            services.AddMemoryCache();
           
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }app.UseRouting();
            app.UseAuthentication();
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
