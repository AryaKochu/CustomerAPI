using CustomerAPI.Bff.Models;
using CustomerAPI.DB;
using CustomerAPI.Services;
using CustomerAPI.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using static CustomerAPI.Swagger.SwaggerConfiguration;

namespace CustomerAPI
{
    public class Startup
    {
        private readonly AppSettings _appSettings;
        public Startup(IConfiguration configuration)
        {
            _appSettings = configuration.Get<AppSettings>();
            ConfigureSwaggerWith(_appSettings);
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddDbContext<CustomersDbContext>(options =>
            {
                options.UseInMemoryDatabase("Customers");
            });
            services.AddSwaggerGen(WithSwaggerGenServiceOptions);
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
            }).UseSwagger(WithSwaggerOptions)
                .UseSwaggerUI(WithSwaggerUiOptions);
        }
    }
}
