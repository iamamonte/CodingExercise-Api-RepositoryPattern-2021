using GroceryStore.Domain.Interface.Manager;
using GroceryStore.Domain.Manager;
using GroceryStore.Infrastructure;
using GroceryStore.Infrastructure.Core;
using GroceryStore.Infrastructure.DataAccess.Interface;
using GroceryStoreAPI.Helpers;
using GroceryStoreAPI.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace GroceryStoreAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            HostingEnvironment = env;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment HostingEnvironment { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSingleton<IDatabaseAccess, DatabaseAccess>();
            services.AddTransient<IRepository<CustomerEntity>, CustomerRepository>();
            services.AddTransient<Serilog.ILogger>(s => new LoggerConfiguration().MinimumLevel.Debug().WriteTo.Debug().CreateLogger());
            services.AddTransient<ILoggerAdapter, SerilogAdapter>();
            services.AddTransient<IGroceryStoreManager, GroceryStoreManager>();
            services.AddSwaggerDocument();
            if (HostingEnvironment.IsDevelopment())
            {
                //configure extended logging
            }

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseOpenApi();
            app.UseSwaggerUi3();
            app.UseAuthorization();
            
            app.UseMiddleware<CustomExceptionMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });   
        }
    }
}
