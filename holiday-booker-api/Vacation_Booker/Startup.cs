using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Vacation_Booker.Entities;
using Vacation_Booker.Repository;

namespace Vacation_Booker
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
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder =>
                    {
                        builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials();
                    });
            });
            services.AddMvc();
            //services.AddCors();

            //connectionstring*********************************************************
            // var connectionstring = "Data Source=DESKTOP-J8HH820\\SQLEXPRESS;Initial Catalog=vacationDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            // Server DB
            //var connectionstring = "Server=localhost\\SQLEXPRESS,1434; Database=vacationDB;Trusted_Connection=True;";


            var connectionstring = "Server=localhost\\SQLEXPRESS;Database=vacationDB;Trusted_Connection=True;";
                      //connectionstring*********************************************************

                     services.AddDbContext<MyContext>(o => o.UseSqlServer(connectionstring));
            services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<MyContext>().AddDefaultTokenProviders();
            services.AddScoped<IDummyRepository, DummyRepository>();

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(cfg =>
            {
                cfg.RequireHttpsMetadata = false;
                cfg.SaveToken = true;


                cfg.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("sdfgsdgrre;n34l5n;sdfgsdfg;dngk34l;wert;wert")),
                    ValidateLifetime = true,
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, MyContext myContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseCors(c =>
            //{
            //    c.AllowAnyOrigin()
            //    .AllowAnyHeader()
            //    .AllowAnyMethod()
            //    .AllowCredentials();
            //});
            app.UseCors("AllowAll");
            app.UseAuthentication();

            app.UseMvc();
        }
    }
}
