using Micke.BlazorBenchmark.GeneralUI.Components.BusyOverlay;
using Micke.BlazorBenchmark.WASM.Shared.Contracts;
using Micke.BlazorBenchmark.WASM.Shared.Entities;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Micke.BlazorBenchmark.GeneralUI.Components
{
  public partial class FileContent : ComponentBase
  {
    [Inject]
    public IPersistenceService PersistenceService { get; set; }

    [Inject]
    public BusyOverlayService BusyOverlayService { get; set; }

    [Parameter]
    public IList<Article> Articles { get; set; } = new List<Article>();

    private long ProcessingTimeMs { get; set; }

    private int ItemCount { get; set; } = 0;

    protected override async Task OnInitializedAsync()
    {
      await PersistenceService.InitAsync();
      await base.OnInitializedAsync();
    }

    private async Task AddTenToPrice()
    {
      BusyOverlayService.SetBusyState(BusyEnum.Busy);
      var stopwatch = new Stopwatch();
      stopwatch.Start();

      // Load all articles from the DB
      Articles = (await PersistenceService.GetAllAsync<Article>()).ToList();
      // Display in table
      StateHasChanged();
      // Add price
      foreach (var article in Articles)
      {
        article.Price += 10;
      }
      // Update all articles in DB
      foreach (var article in Articles)
      {
        await PersistenceService.UpdateAsync<Article>(article);
      }

      stopwatch.Stop();
      ProcessingTimeMs = stopwatch.ElapsedMilliseconds;
      BusyOverlayService.SetBusyState(BusyEnum.NotBusy);
    }

    private void HandleClearTable()
    {
      BusyOverlayService.SetBusyState(BusyEnum.Busy);
      var stopwatch = new Stopwatch();
      stopwatch.Start();

      Articles.Clear();

      ItemCount = 0;

      stopwatch.Stop();
      ProcessingTimeMs = stopwatch.ElapsedMilliseconds;
      BusyOverlayService.SetBusyState(BusyEnum.NotBusy);
    }

    private async Task HandleLoadArticlesFromIndexedDb()
    {
      BusyOverlayService.SetBusyState(BusyEnum.Busy);
      var stopwatch = new Stopwatch();
      stopwatch.Start();

      var result = await PersistenceService.GetAllAsync<Article>();

      Articles = result.ToList();

      ItemCount = Articles.Count();

      stopwatch.Stop();
      ProcessingTimeMs = stopwatch.ElapsedMilliseconds;
      BusyOverlayService.SetBusyState(BusyEnum.NotBusy);
    }

    private async Task HandleAddToIndexedDb()
    {
      BusyOverlayService.SetBusyState(BusyEnum.Busy);
      var stopwatch = new Stopwatch();
      stopwatch.Start();

      foreach (var article in Articles)
      {
        await PersistenceService.DeleteAsync<Article>(article);
        ItemCount = Math.Max(0, ItemCount--);
      }

      ItemCount = 0;
      foreach (var article in Articles)
      {
        await PersistenceService.InsertAsync<Article>(article);
        ItemCount++;
      }

      StateHasChanged();
      stopwatch.Stop();
      ProcessingTimeMs = stopwatch.ElapsedMilliseconds;
      BusyOverlayService.SetBusyState(BusyEnum.NotBusy);
    }

    private async Task HandleDeleteFromIndexedDb()
    {
      BusyOverlayService.SetBusyState(BusyEnum.Busy);
      var stopwatch = new Stopwatch();
      stopwatch.Start();

      ItemCount = Articles.Count;
      StateHasChanged();
      foreach (var article in Articles)
      {
        await PersistenceService.DeleteAsync<Article>(article);
        ItemCount = Math.Max(0, ItemCount--);
      }

      StateHasChanged();
      stopwatch.Stop();
      ProcessingTimeMs = stopwatch.ElapsedMilliseconds;
      BusyOverlayService.SetBusyState(BusyEnum.NotBusy);
    }
  }
}
