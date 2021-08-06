using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using FoodOrderApplicationAPI.AuthorizationScopes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodOrderApplicationAPI.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace FoodOrderApplicationAPI
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

            //database context
            services.AddDbContext<MenuDbContext>(options => options.UseSqlite(Configuration["SqliteConnectionString"]));

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "FoodOrderApplicationAPI", Version = "v1" });


                //lets allow Swagger to put security tokens.
                var securityScheme = new OpenApiSecurityScheme()
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT"
                };

                var securityRequirement = new OpenApiSecurityRequirement
                {
                    {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "bearerAuth"
                                }
                            },
                            new string[] {}
                    }
                };
                c.AddSecurityDefinition("bearerAuth", securityScheme);
                c.AddSecurityRequirement(securityRequirement);

            });

            // 1. Add Authentication Services
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.Authority = Configuration["Authority"];
                options.Audience = Configuration["Audience"];
            });

            // Register the scope authorization handler
            services.AddSingleton<IAuthorizationHandler, HasScopeHandler>();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("testperm1", policy => policy.Requirements.Add(new HasScopeRequirement("read:testperm1", "")));
                options.AddPolicy("testperm2", policy => policy.Requirements.Add(new HasScopeRequirement("read:testperm2", "")));

                //policy related to TestRoleOrderApi Role
                options.AddPolicy("TestRoleOrderApi", policy =>
                {
                    policy.Requirements.Add(new HasScopeRequirement("read:testperm1", ""));
                    policy.Requirements.Add(new HasScopeRequirement("read:testperm2", ""));
                });

                //policy related to Admin Role
                options.AddPolicy("Admin", policy =>
                {
                    policy.Requirements.Add(new HasScopeRequirement("read:adminpage", ""));
                });

                //policy related to KitchenWorker Role
                options.AddPolicy("KitchenWorker", policy =>
                {
                    policy.Requirements.Add(new HasScopeRequirement("read:kitchenworkerpage", ""));
                });

                //policy related to Waiter Role
                options.AddPolicy("Waiter", policy =>
                {
                    policy.Requirements.Add(new HasScopeRequirement("read:waiterpage", ""));
                });


                //policy related to Customer Role
                options.AddPolicy("Customer", policy =>
                {
                    policy.Requirements.Add(new HasScopeRequirement("read:customerpage", ""));
                });



            });



        }



        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "FoodOrderApplicationAPI v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            // 2. Enable authentication middleware
            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
