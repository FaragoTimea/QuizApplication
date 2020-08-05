using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Serialization;
using QuizApplication.Data;
using QuizApplication.Interfaces;
using QuizApplication.Services;

namespace QuizApplication
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
            services.AddSingleton<IQuizChecker>(new QuizChecker());

            services.AddAutoMapper(typeof(Startup));
            services.AddDbContext<QuizDbContext>(options =>
                //choose
                options.UseSqlServer(Configuration.GetConnectionString("QuizMsSqlDb"))
                //options.UseMySql(Configuration.GetConnectionString("QuizMySqlDb"))
                );

            //json serializer
            services.AddControllers().AddNewtonsoftJson(options => {
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
            });

            //swagger
            services.AddSwaggerDocument(document =>
            {
                document.PostProcess = d =>
                {
                    d.Info.Title = "Quiz application";
                };
            });
        }
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

            //swagger
            app.UseOpenApi();
            app.UseSwaggerUi3();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
