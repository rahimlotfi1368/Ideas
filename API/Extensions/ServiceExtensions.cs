using API.Middlewares;
using AutoMapper;
using AutoWrapper;
using Domain.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using Services.Administration;
using Services.Administration.AdministrationMapper;
using Services.Authentication;
using Services.EFConfigurations;
using System.Text;


namespace API.Extensions
{
    public static class ServiceExtensions
    {
        public static void Configuredatabasecontext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DataBaseContext>(options =>
            {
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                options.UseSqlServer(configuration.GetConnectionString("DataBaseContext"));
            });
        }
        public static void ConfigureIdentity(this IServiceCollection services)
        {
            var builder = services.AddIdentity<User, IdentityRole>(o =>
            {
                o.Password.RequireDigit = true;
                o.Password.RequireLowercase = false;
                o.Password.RequireUppercase = false;
                o.Password.RequireNonAlphanumeric = false;
                o.Password.RequiredLength = 8;
                o.User.RequireUniqueEmail = true;
            })
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<DataBaseContext>()
            .AddDefaultTokenProviders();
        }

        public static void ConfigureMyJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
          .AddJwtBearer(options =>
          {
              options.RequireHttpsMetadata = false;
              options.SaveToken = true;
              options.TokenValidationParameters = new TokenValidationParameters
              {
                  ValidateIssuer = false,
                  ValidateAudience = false,
                  ValidateLifetime = true,
                  ValidateIssuerSigningKey = true,
                  IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"])),
                  ClockSkew = TimeSpan.Zero
              };
          });
        }

        public static void ConfigureMyServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IAdministrationService, AdministrationService>();
            services.AddHttpContextAccessor();
        }

        public static void ConfigureMyMappers(this IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AdminMapper());
            });

            IMapper mapper = mapperConfig.CreateMapper();

            services.AddSingleton(mapper);
        }

        public static void ConfigureMyCores(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyHeader()
                    .WithMethods("PUT", "DELETE", "GET", "POST","OPTIONS")
                    .WithHeaders(HeaderNames.AccessControlAllowOrigin,HeaderNames.Allow)
                    .AllowAnyOrigin());

                //.WithOrigins("http://127.0.0.1:5500")); //front app localHost
            });
        }

        public static void ConfigureMySwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Ideas.Api", Version = "v1-0-2" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please insert JWT with Bearer into field",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });

                //var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                //var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                //c.IncludeXmlComments(xmlPath, true);

                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                           {
                             new OpenApiSecurityScheme
                                 {
                                   Reference = new OpenApiReference
                                       {
                                         Type = ReferenceType.SecurityScheme,
                                         Id = "Bearer"
                                       }
                                  },
                              new string[] { }
                           }
                });
            });
        }


        //*******************************************************************************************
        public static void UseCustomSwagger(this IApplicationBuilder app)
        {
            app.UseSwagger();

            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Ideas.Api v1"));
        }

        public static void UseMyApiResponseAndExceptionWrapper(this IApplicationBuilder app)
        {
            app.UseApiResponseAndExceptionWrapper(new AutoWrapperOptions
            {
                IsDebug = true,
                IsApiOnly = false,
                WrapWhenApiPathStartsWith = "/swagger/index.html"
            });

        }

        public static void UseMyStaticFiles(this IApplicationBuilder app)
        {
            app.UseStaticFiles();

            //app.UseStaticFiles(new StaticFileOptions()
            //{
            //    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Images")),
            //    RequestPath = new PathString("/wwwroot/Images")
            //});
        }

        public static IApplicationBuilder UseOptions(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<OptionsMiddleware>();
        }
    }
}

