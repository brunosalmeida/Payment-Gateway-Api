using System;
using System.Reflection;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Converters;
using Paymentgateway.Application.Commands;
using PaymentGateway.Data.Repositories;
using PaymentGateway.Domain.Interfaces;
using PaymentGateway.Infrastructure.AcquiringBank;
using PaymentGateway.Infrastructure.Cache;
using PaymentGateway.Infrastructure.Resilience;

namespace PaymentGateway.Api
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
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Version = "v1",
                        Title = "Payment Gateway",
                        Description = "Payment Gateway API",
                        Contact = new OpenApiContact
                        {
                            Name = "Bruno Almeida",
                            Email = string.Empty,
                            Url = new Uri("https://github.com/brunosalmeida"),
                        }
                    });
            });

            services.AddControllers()
                .AddNewtonsoftJson((options => 
                    options.SerializerSettings.Converters.Add(new StringEnumConverter())));
            
            services.AddMediatR(typeof(Startup));
            services.AddMediatR(typeof(PaymentCommand).GetTypeInfo().Assembly);
            
            services.AddTransient<IPaymentRepository, PaymentRepository>();
            services.AddTransient<IPaymentRepositoryResiliencePolicy, PaymentRepositoryResiliencePolicy>();
            services.AddTransient<ICache, Cache>();
            services.AddTransient<IAcquiringBank, Infrastructure.AcquiringBank.AcquiringBank>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
  
            app.UseSwagger(c =>
            {
                c.SerializeAsV2 = true;
            });

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Payment Gateway API");
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}