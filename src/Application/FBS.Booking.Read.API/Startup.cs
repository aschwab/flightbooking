using EventStore.ClientAPI;
using FBS.Domain.Booking;
using FBS.Domain.Core;
using FBS.Infrastructure.AspNet;
using FBS.Infrastructure.EventStore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Reflection;

namespace FBS.Booking.Read.API
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
            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Booking Read API", Version = "v1" });
            });

            services.AddControllers();
            //configure event store
            services.AddSingleton<Func<IEventStoreConnection>>(_ =>
                {
                    var eventStoreAppSettings = Configuration.GetSection("EventStore");
                    var connectionSettings = ConnectionSettings.Create();
                    connectionSettings.SetHeartbeatInterval(TimeSpan.FromSeconds(40));
                    connectionSettings.SetHeartbeatTimeout(TimeSpan.FromSeconds(20));
                    connectionSettings.SetDefaultUserCredentials(
                        new EventStore.ClientAPI.SystemData
                            .UserCredentials(eventStoreAppSettings.GetValue<String>("User"),
                                eventStoreAppSettings.GetValue<String>("Password")));

                    return () =>
                    {
                        var connection = EventStoreConnection.Create(connectionSettings,
                            new Uri(eventStoreAppSettings.GetValue<String>("Endpoint")));

                        connection.ConnectAsync().Wait();
                        return connection;
                    };
                }
            );

            services.AddSingleton<IEventStore, EventStoreEventStore>();

            //register contexts for read model
            services.AddSingleton(typeof(IAggregateContext<>), typeof(InMemoryContext<>));

            //register event distributor
            services.AddSingleton((sp) => new EventDistributorSettings() { StartFromBeginning = true });
            services.AddSingleton<IEventDistributor, EventStoreEventDistributor>();

            //register mediator for CommandHandler Injection
            services.AddMediatR(Assembly.GetExecutingAssembly());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Booking Read API V1");
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(builder => builder.AllowAnyOrigin());

            //app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            //initialize event distributor
            var eventDistributor = app.ApplicationServices.GetService<IEventDistributor>();
            eventDistributor.StartDistributing();
        }
    }
}