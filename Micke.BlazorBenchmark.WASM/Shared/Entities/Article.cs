using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Micke.BlazorBenchmark.WASM.Shared.Entities
{
  public class Article : BaseItem
  {
    public string Description { get; set; }
    public string SupplierId { get; set; }
    public int MainGroupId { get; set; }
    public int SubGroupId { get; set; }
    public decimal Price { get; set; }
    public string Ean { get; set; }
  }
}
