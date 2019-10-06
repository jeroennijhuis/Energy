using Amazon.CognitoIdentityProvider;
using Amazon.DynamoDBv2;
using Energy.Core.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NSwag;
using NSwag.Generation.Processors.Security;

namespace Energy.API
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddDefaultAWSOptions(Configuration.GetAWSOptions());

            //Swagger
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

                // Add an authenticate button to Swagger for JWT tokens
                config.OperationProcessors.Add(new OperationSecurityScopeProcessor("JWT"));
                config.DocumentProcessors.Add(new SecurityDefinitionAppender("JWT", new OpenApiSecurityScheme
                {
                    Type = OpenApiSecuritySchemeType.ApiKey,
                    Name = "Authorization",
                    In = OpenApiSecurityApiKeyLocation.Header,
                    Description = "Type into the text box: 'Bearer {TOKEN}'. You can get a JWT token from: " +
                    "https://cognito-idp.eu-central-1.amazonaws.com/eu-central-1_7wrt2SCUs"
                }));
            });

            //AWS Cognito
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Audience = "34ciupnqfed4l7gga4r0mq9sd6";
                    options.Authority = "https://cognito-idp.eu-central-1.amazonaws.com/eu-central-1_7wrt2SCUs";
                });
            services.AddAWSService<IAmazonCognitoIdentityProvider>();

            //AWS DynamoDB
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
            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
