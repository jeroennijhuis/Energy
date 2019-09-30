using Amazon.DynamoDBv2;
using Energy.Core.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Energy.API
{
    public class Startup
    {
        //todo authentication
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            // Register the Swagger services
            services.AddSwaggerDocument(config =>
            {
                config.PostProcess = document =>
                {
                    document.Info.Version = "v1";
                    document.Info.Title = "Energy API";
                    document.Info.Description = "https://github.com/jeroennijhuis/Energy-Insight";
                    document.Info.Contact = new NSwag.OpenApiContact
                    {
                        Name = "Jeroen Nijhuis"
                    };
                    document.Info.License = new NSwag.OpenApiLicense
                    {
                        Name = "License",
                        Url = "https://github.com/jeroennijhuis/Energy-Insight/blob/master/LICENSE"
                    };
                };
            });

            //AWS DynamoDB
            services.AddDefaultAWSOptions(Configuration.GetAWSOptions());
            services.AddAWSService<IAmazonDynamoDB>();
            services.AddSingleton<IEnergyRepository, Repository.DynamoDb.EnergyRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
                app.UseHsts();

            // Register the Swagger generator and the Swagger UI middleware
            app.UseOpenApi();
            app.UseSwaggerUi3();

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
