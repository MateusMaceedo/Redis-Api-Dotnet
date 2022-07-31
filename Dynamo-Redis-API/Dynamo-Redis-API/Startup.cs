using Amazon;
using Amazon.DynamoDBv2;
using Dynamo_Redis_API.Domain.Interfaces;
using Dynamo_Redis_API.Infra.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using StackExchange.Redis;
using System;

namespace Dynamo_Redis_API
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
            services.AddControllersWithViews();
            services.AddControllers();
            services.AddSwaggerGen();

            services.AddSingleton<IAmazonDynamoDB>(_ =>
            new AmazonDynamoDBClient(
            RegionEndpoint.GetBySystemName(Configuration.GetValue<string>("DynamoDb:Region"))));
            services.AddSingleton<IConnectionMultiplexer>(x =>
            ConnectionMultiplexer.Connect(Configuration.GetValue<string>("Redis:ConnectionString")));

            services.AddSingleton<IProductRepository>(x =>
            new ProductRepository(
            x.GetRequiredService<IAmazonDynamoDB>(),
            x.GetRequiredService<IConnectionMultiplexer>(), Configuration.GetValue<string>("DynamoDb:TableName")));

            services.AddDefaultAWSOptions(Configuration.GetAWSOptions("DynamoDb"));
            services.AddSingleton<IProductRepository, ProductRepository>();
            services.AddAWSService<IAmazonDynamoDB>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Template Dynamo e Redis",
                    Description = "API product",
                    TermsOfService = new Uri("https://example.com/terms")
                });
            });
         }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseSwagger();
            app.UseSwaggerUI(setup =>
            {
                setup.RoutePrefix = "swagger";
                setup.SwaggerEndpoint("/swagger/v1/swagger.json", "Api Documentation");
            });

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
