using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Quiz.Business.Abstract;
using Quiz.Business.Concrete;
using Quiz.Business.Mapping;
using Quiz.Business.Security.Microsoft.Jwt.Concrete;
using Quiz.DataAccess.Abstract;
using Quiz.DataAccess.Concrete;
using Quiz.Entities.Context;
using Quiz.UnitOfWork.Abstract;
using Quiz.UnitOfWork.Concrete;
using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Quiz.Business.Security.Microsoft.Jwt.Abstract;
using Quiz.Dto.Jwt;

namespace Quiz.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            var tokenOptions = Configuration.GetSection("TokenOptions").Get<JwtTokenOptionsDto>();
            if (tokenOptions != null)
            {
                services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = true,// Token gönderen kişi doğrula.  App dosyası içindeki  Audience bilgisini 
                        ValidateIssuer = true,  // Token gönderen kişi doğrula.  App dosyası içindeki  Issuer bilgisini 
                        ValidateLifetime = true,// Token ömrünü kontrol et ediyoruz. 
                        ValidateIssuerSigningKey = true, // Token app setting içindeki security key ile mi imzalanmış onu kontrol eder
                        ValidIssuer = tokenOptions.Issuer,
                        ValidAudience = tokenOptions.Audience,
                        IssuerSigningKey = SignHandler.GetSecurityKey(tokenOptions.SecurityKey), // Gelen token  bizim security key'imiz ile işaretlenmiş mi onun kontrolünü sağladık
                        ClockSkew = TimeSpan.Zero // Uygulamanın atılan sunucular arasındaki zaman farkını gidermek için verilir tanımlanan değer kadar mevcut token süresine ekler
                    };
                });
            }
            services.AddMvc();
            services.AddRazorPages();
            services.AddDbContext<QuizContext>(contextOptions => contextOptions
                .UseSqlServer(Configuration.GetConnectionString("QuizContextConString")));
            services.AddScoped<IQuizService, QuizManager>();
            services.AddScoped<IQuizDal, QuizDal>();
            services.AddScoped<IUserDal, UserDal>();
            services.AddScoped<IUserService, UserManager>();
            services.AddScoped<IToken, Token>();
            services.AddScoped<IUnitOfWork, BaseUnitOfWork>();
            services.AddScoped<IAuthenticationService, AuthenticationManager>();
            services.AddScoped<QuizContext>();
            var mappingConfig = new MapperConfiguration(mc => { mc.AddProfile(new QuizMapper()); });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
            /*
             * Tüm isteklere cevap vermesi  için gerekli Cors tanımı.
             */
            services.AddCors(option => option.AddDefaultPolicy
                (builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

            #region Cors servis tanım 2

            /*
             * Sadece wwww.evrenaktas.com'dan gelen isteklere cevap vermesi için gerekli Cors tanımı
             */
            //services.AddCors(option => option.AddPolicy("Policy",
            //    builder =>
            //    {
            //        builder.WithOrigins("http://wwww.evrenaktas.com")
            //        .AllowAnyHeader().AllowAnyOrigin();
            //    }));

            #endregion

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Quiz Web Api",
                    Version = "1.0.0",
                    Description = "Try Swagger on (ASP.NET Core 3.1)",
                    Contact = new OpenApiContact
                    {
                        Name = "Swagger Implementation Evren Aktaş",
                        Url = new Uri("http://evrenaktas.com"),
                        Email = "evren.aktas@outlook.com"
                    },
                    TermsOfService = new Uri("http://swagger.io/terms/")

                });
            });
            services.Configure<JwtTokenOptionsDto>(Configuration.GetSection("TokenOptions"));
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseRouting();
            #region Cors tanım 2 middlaware
            //app.UseCors("Policy"); Eklenen Cors tanımının middleware olarak eklenmesi
            #endregion
            app.UseCors();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
            app.UseAuthentication();
            app.UseAuthorization();
           
            app.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllerRoute(name: "default", pattern: "{api}/{controller=home}/{action=index}");
                });
        }
    }
}
