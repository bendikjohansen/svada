using System.Data;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Npgsql;
using Pemacy.Svada.Generator.Repositories;
using Pemacy.Svada.Generator.Services.Category;
using Pemacy.Svada.Generator.Services.Sentence;
using Pemacy.Svada.Generator.Services.Word;
using Pemacy.Svada.Generator.Web.Controllers.Category;
using Pemacy.Svada.Generator.Web.Controllers.Word;

namespace Pemacy.Svada.Generator.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private readonly IConfiguration _configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
                })
                .AddFluentValidation(config => config.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly()));

            services
                .AddSingleton<ICategoryConverter, CategoryConverter>()
                .AddSingleton<ICategoryService, CategoryService>()
                .AddSingleton<ICategoryRepository, CategoryRepository>()
                .AddSingleton<IWordService, WordService>()
                .AddSingleton<IWordConverter, WordConverter>()
                .AddSingleton<IWordRepository, WordRepository>()
                .AddSingleton<ISentenceGenerator, SentenceGenerator>()
                .AddTransient<IDbConnection>(provider => new NpgsqlConnection(_configuration.GetConnectionString("main")));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.ConfigureDatabase();

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
