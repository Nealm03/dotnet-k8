using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace dotnetk8
{

  public class Startup
  {
    readonly IConfiguration configuration;
    public Startup(IConfiguration config)
    {
      configuration = config ?? throw new NullReferenceException("Invalid arg");
    }
    // This method gets called by the runtime. Use this method to add services to the container.
    // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddHealthChecks();
      services.AddControllers();
      services.Configure<Config>(configuration.GetSection("Config"));
      services.Configure<ForwardedHeadersOptions>(options =>
      {
        options.ForwardedHeaders =
              ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
      });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.UseForwardedHeaders(); 
      app.UseRouting();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapHealthChecks("/health/startup");
        endpoints.MapHealthChecks("/healthy");
        endpoints.MapHealthChecks("/ready");
        endpoints.MapGet("/", async context =>
        {
          var config = context.RequestServices.GetService<IOptions<Config>>();
          var msg = $"hello {config.Value.Env}";
          await context.Response.WriteAsync(msg);
        });
        endpoints.MapControllers();
      });
    }
  }
}
