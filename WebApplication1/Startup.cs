using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1 // как зарегать generic, зарегать тот Baserepository, 
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

            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            var appconfig = config.Get<AppConfig>();

            services.AddSingleton(appconfig);

            services.AddTransient(typeof(BaseRepository<>));

            services.AddSingleton<UserService>();
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

    public class AppConfig
    {
        public MongoConnectionConfig MongoConnection { get; set; }
    }

    public class MongoConnectionConfig
    {
        public string ConnectionString { get; set; }

        public string DatabaseName { get; set; }
    }
}
//у меня должен быть database provider(класс), у него метод, который возвращает getcollection(generic)(должен принимать connection string) и я должен resolveить databaseprovider в DBrepository И возвращать в коллекцию
//