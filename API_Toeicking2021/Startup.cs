using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API_Toeicking2021.Data;
using API_Toeicking2021.Services.SentenceDBService;
using API_Toeicking2021.Services.UserDBService;
using API_Toeicking2021.Services.VocabularyDBService;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace API_Toeicking2021
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
            // ª`¤JDbContext¡GAddDbContext<DataContext>
            services.AddDbContext<DataContext>(x => x.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddControllers();
            // ª`¤J AUTOMAPPER
            services.AddAutoMapper(typeof(Startup));
            // ª`¤JDB service
            services.AddScoped<ISentenceDBService, SentenceDBService>();
            services.AddScoped<IUserDBService, UserDBService>();
            services.AddScoped<IVocabularyDBService, VocabularyDBService>();
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

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
