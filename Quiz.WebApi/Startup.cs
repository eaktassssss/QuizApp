using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Quiz.Business.Abstract;
using Quiz.Business.Concrete;
using Quiz.Business.Mapping;
using Quiz.DataAccess.Abstract;
using Quiz.DataAccess.Concrete;
using Quiz.Entities.Context;
using Quiz.UnitOfWork.Abstract;
using Quiz.UnitOfWork.Concrete;
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
            services.AddMvc();
            services.AddRazorPages();
            services.AddDbContext<QuizContext>(contextOptions => contextOptions
                .UseSqlServer(Configuration.GetConnectionString("QuizContextConString")));
            services.AddScoped<IQuizService, QuizManager>();
            services.AddScoped<IQuizDal, QuizDal>();
            services.AddScoped<IUnitOfWork, BaseUnitOfWork>();
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
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Quiz Web Api", Version = "1.0.0",
                    Description = "Try Swagger on (ASP.NET Core 3.1)",
                    Contact = new OpenApiContact
                {
                    Name = "Swagger Implementation Evren Aktaş",
                    Url = new Uri("http://evrenaktas.com"),
                    Email = "evren.aktas@outlook.com"
                },
                    TermsOfService=new Uri("http://swagger.io/terms/")
                  
                });
            });

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
            app.UseAuthorization();
            
            app.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllerRoute(name: "default", pattern: "{api}/{controller=home}/{action=index}");
                });
        }
    }
}
