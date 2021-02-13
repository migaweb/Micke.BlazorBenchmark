using Micke.BlazorBenchmark.GeneralUI;
using Micke.BlazorBenchmark.GeneralUI.Components.BusyOverlay;
using Micke.BlazorBenchmark.Services.Services;
using Micke.BlazorBenchmark.WASM.Shared.Contracts;
using Micke.BlazorBenchmark.WASM.Shared.Entities;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Micke.BlazorBenchmark.WASM.Client
{
  public class Program
  {
    public static async Task Main(string[] args)
    {
      var builder = WebAssemblyHostBuilder.CreateDefault(args);
      builder.RootComponents.Add<App>("#app");

      builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
      builder.Services.AddScoped<IPersistenceService, IndexedDB>();
      builder.Services.AddScoped<IExcelReader<Article>, ArticleExcelReader>();
      builder.Services.AddScoped<BusyOverlayService>();

      await builder.Build().RunAsync();
    }
  }
}
