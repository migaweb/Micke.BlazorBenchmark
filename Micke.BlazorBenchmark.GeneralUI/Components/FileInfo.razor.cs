using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Micke.BlazorBenchmark.GeneralUI.Components
{
  public partial class FileInfo : ComponentBase
  {
    [Parameter]
    public IBrowserFile File { get; set; }
    
    [Parameter]
    public int ArticlesCount { get; set; }
    
    [Parameter]
    public long ProcessingTimeMs { get; set; }
  }
}
