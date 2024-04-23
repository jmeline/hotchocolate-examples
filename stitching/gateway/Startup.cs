using System;
using HotChocolate.Types;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Gateway;

public class Startup
{
    public const string Products = "products";

    // This method gets called by the runtime. Use this method to add services to the container.
    // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddHttpClient(Products, c => c.BaseAddress = new Uri("http://localhost:5053/graphql"));

        services
            .AddGraphQLServer()
            .AddQueryType(d => d.Name("Query"))
            .AddRemoteSchema(Products, ignoreRootTypes: true)
            .BindRuntimeType<DateOnly, DateType>()
            .AddTypeConverter<DateOnly, DateTime>(from => from.ToDateTime(default))
            .AddTypeConverter<DateTime, DateOnly>(from => DateOnly.FromDateTime(from.Date))
            .AddTypeExtensionsFromFile("./Stitching.graphql");
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseRouting();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapGraphQL();
        });
    }
}