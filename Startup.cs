using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using preAceleracionDisney.Context;
using preAceleracionDisney.Entities;
using preAceleracionDisney.Interfaces;
using preAceleracionDisney.Repositories;
using preAceleracionDisney.Services;
using SendGrid.Extensions.DependencyInjection;

using System.Collections.Generic;
using System.Text;


namespace preAceleracionDisney
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
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "preAceleracionDisney", Version = "v1" });

                //Configuracion del swagger para Autorizar
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = @"Insert token. Example: 'Bearer 12345abcdef'",
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                 {
                     {
                           new OpenApiSecurityScheme
                             {
                                 Reference = new OpenApiReference
                                 {
                                     Type = ReferenceType.SecurityScheme,
                                     Id = "Bearer"
                                 }
                             },
                             new string[] {}
                     }
                 });
            });

            services.AddEntityFrameworkSqlServer();

            //Di de Identity

            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<UserContext>()
                .AddDefaultTokenProviders();

            //Auth con JWT

            var key = Encoding.ASCII.GetBytes(Configuration.GetValue<string>("JwtKey"));
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.SaveToken = true;
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = false,
                        ValidateIssuer = false,
                        ValidAudience = "https://localhost:5001",
                        ValidIssuer = "https://localhost:5001",
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuerSigningKey = true
                    };
                });

            //Di de contextos
            services.AddDbContext<DisneyDbContext>((services, options) =>
            {
                options.UseInternalServiceProvider(services);
                options.UseSqlServer(Configuration.GetConnectionString("DisneyConecctionString"));
            });

            services.AddDbContext<UserContext>((services, options) =>
            {
                options.UseInternalServiceProvider(services);
                options.UseSqlServer(Configuration.GetConnectionString("UsersConecctionString"));
            });

            // DI de repositorios:
            services.AddScoped<ICharacterRepository, CharacterRepository>();
            services.AddScoped<IGenderRepository, GenderRepository>();
            services.AddScoped<IMovieRepository, MovieRepository>();

            // DI SendGrid
            services.AddSendGrid(options =>
            { 
                options.ApiKey = Configuration["SenderGridApiKey"];
            });

            services.AddScoped<IMailService, MailService>();

           
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
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();

            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "preAceleracionDisney v1"));
        }
    }
}
