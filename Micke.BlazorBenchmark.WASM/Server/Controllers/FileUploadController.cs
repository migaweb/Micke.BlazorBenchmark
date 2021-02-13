using Micke.BlazorBenchmark.WASM.Shared;
using Micke.BlazorBenchmark.WASM.Shared.Contracts;
using Micke.BlazorBenchmark.WASM.Shared.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Micke.BlazorBenchmark.WASM.Server.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class FileUploadController : ControllerBase
  {
    private readonly IExcelReader<Article> _excelReader;

    public FileUploadController(IExcelReader<Article> excelReader)
    {
      _excelReader = excelReader;
    }

    [HttpPost]
    public async Task<IActionResult> Post(UploadedFile uploadedFile)
    {
      var fileBytes = uploadedFile.FileContent;
      IList<Article> result = new List<Article>();

      using (var reader = new System.IO.StreamReader(new MemoryStream(fileBytes))) {
        result = await _excelReader.ReadAsync(reader);
      }

      return Ok(result);
    }
  }
}
