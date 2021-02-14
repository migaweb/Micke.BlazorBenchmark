using Micke.BlazorBenchmark.GeneralUI;
using Micke.BlazorBenchmark.GeneralUI.Components.BusyOverlay;
using Micke.BlazorBenchmark.Services.Services;
using Micke.BlazorBenchmark.WASM.Shared.Contracts;
using Micke.BlazorBenchmark.WASM.Shared.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Micke.BlazorBenchmark.SERV
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddScoped(sp => new HttpClient());

      //https://docs.microsoft.com/en-us/aspnet/core/signalr/streaming?view=aspnetcore-3.1
      // There is a problem sending "large" chunks of data on the signalr connection
      // Using the solution on this stack overflow page for now to increase
      // max receive message size:
      //https://stackoverflow.com/questions/60311852/error-connection-disconnected-with-error-error-server-returned-an-error-on-cl
      services.AddSignalR(e => {
        e.MaximumReceiveMessageSize = 102400000;
      });

      services.AddRazorPages();
      //services.AddServerSideBlazor();

      services.AddServerSideBlazor(options =>
      {
        options.DetailedErrors = true;
        options.DisconnectedCircuitMaxRetained = 100;
        options.DisconnectedCircuitRetentionPeriod = TimeSpan.FromMinutes(3);
        options.JSInteropDefaultCallTimeout = TimeSpan.FromMinutes(1);
        options.MaxBufferedUnacknowledgedRenderBatches = 10;
      });

      services.AddScoped<IPersistenceService, IndexedDB>();
      services.AddScoped<IExcelReader<Article>, ArticleExcelReader>();
      services.AddScoped<BusyOverlayService>();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }
      else
      {
        app.UseExceptionHandler("/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
      }

      app.UseHttpsRedirection();
      app.UseStaticFiles();

      app.UseRouting();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapBlazorHub();
        endpoints.MapFallbackToPage("/_Host");
      });
    }
  }
}
