using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using MiniStore.Context;
using MiniStore.Repositories;
using MiniStore.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MiniStore
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
            services.AddDbContext<StoreContext>(options => options.UseSqlServer(Configuration.GetConnectionString("StoreConnStr")));
            services.AddControllers();
            services.AddCors(options =>
            {
                options.AddPolicy(
                    name: "AllowOrigin",
                    builder =>
                    {
                        builder.WithOrigins("https://localhost:44351", "http://localhost:4200", "http://localhost:7183")
                                            .AllowAnyHeader()
                                            .AllowAnyMethod()
                                            .AllowCredentials();
                    });
            });
            services.AddAutoMapper(typeof(Startup));
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("V1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Version = "V1",
                    Title = "MiniStore API",
                    Description = "Api for managing a mini store"
                });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                s.IncludeXmlComments(xmlPath);
            });
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes
                        (Configuration.GetSection("AppSettings:Token").Value)
                        ),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });



        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                IdentityModelEventSource.ShowPII = true;
            }
            if (env.IsProduction())
            {
                // Production code ....!
            }
            

            
            app.UseAuthentication();  
            app.UseRouting();
            app.UseCors("AllowOrigin");
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("swagger/V1/swagger.json", "Ministore Api V1");
                c.RoutePrefix = string.Empty;
            });
        }
    }
}
