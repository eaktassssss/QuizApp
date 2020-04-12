using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
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
            services.AddScoped<IUnitOfWork,BaseUnitOfWork>();
            services.AddScoped<QuizContext>();
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new QuizMapper());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllerRoute(name: "default", pattern: "{api}/{controller=home}/{action=index}");
                });
        }
    }
}
