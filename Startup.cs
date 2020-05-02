using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProductCatalog.Data;
using ProductCatalog.Repositories;
using ProductCatalog.Repositories.Contracts;

namespace ProductCatalog
{
    public class Startup
    {
         public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(option => option.EnableEndpointRouting = false);         


            // Um objeto por requisição
            services.AddScoped<StoreDataContext, StoreDataContext>();
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();

            services.AddResponseCompression();
            services.AddResponseCaching();

            services.AddSwaggerGen(setup => {
                setup.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo() { Title = "Gusta Store", Version="v1"});
            });

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();

            app.UseResponseCompression();
            app.UseResponseCaching();

            app.UseSwagger();
            app.UseSwaggerUI(setup => 
            {
                setup.SwaggerEndpoint("/swagger/v1/swagger.json", "Gusta Store - v1");
            });
        }
    }
}
