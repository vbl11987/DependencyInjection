using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DependencyInjection.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using DependencyInjection.Models;

namespace DependencyInjection
{
    public class Startup
    {
        private IHostingEnvironment env;

        public Startup(IHostingEnvironment env)
        {
            this.env = env;

            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            TypeBroker.SetRepositoryType<AlternativeRepository>();
            //services.AddTransient<IRepository, MemoryRepository>();

            //using the scoped life cicle, using this the same MemoryRepository is used for any class that need a 
            //implementation of it in the same scope
            services.AddScoped<IRepository, MemoryRepository>();

            //using singleton life cycle, a single object is used to resolve all the dependencies for a given
            //service and it's reused for any subsquent need of dependecies.
            services.AddSingleton<IRepository, MemoryRepository>();

            //Using a Factory function
            services.AddTransient<IRepository>(provider => {
                if(env.IsDevelopment()) {
                    var x = provider.GetService<MemoryRepository>();
                    return x;
                } else {
                    return new AlternativeRepository();
                }
            });
            //MemoryReposotry have dependencies from IModelStorage, so is required to add a Transient
            //to resolve any dependencias that it haves.
            services.AddTransient<MemoryRepository>();

            services.AddTransient<IModelStorage, DictionaryStorage>();
            services.AddTransient<ProductTotalizer>();
            // Add framework services.
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseStatusCodePages();
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvcWithDefaultRoute();
        }
    }
}
