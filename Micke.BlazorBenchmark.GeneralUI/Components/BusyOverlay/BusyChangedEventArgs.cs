using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Micke.BlazorBenchmark.GeneralUI.Components.BusyOverlay
{
  public class BusyChangedEventArgs : EventArgs
  {
    public BusyEnum BusyState { get; set; }
  }
}
