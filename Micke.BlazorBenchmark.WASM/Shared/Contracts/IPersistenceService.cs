using Micke.BlazorBenchmark.WASM.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Micke.BlazorBenchmark.WASM.Shared.Contracts
{
  public interface IPersistenceService
  {
    Task<IEnumerable<T>> GetAllAsync<T>() where T : BaseItem;
    Task<IEnumerable<T>> GetAsync<T>(Expression<Func<T, bool>> whereExpression) where T : BaseItem;
    Task<int> InsertAsync<T>(T entity) where T : BaseItem;
    Task UpdateAsync<T>(T entity) where T : BaseItem;
    Task DeleteAsync<T>(T entity) where T : BaseItem;
    Task InitAsync();
  }
}
