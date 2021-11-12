using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Micke.BlazorBenchmark.WASM.Shared.Contracts
{
  public interface IExcelReader<T> where T : class
  {
    IList<T> Read(Stream stream);
  }
}
