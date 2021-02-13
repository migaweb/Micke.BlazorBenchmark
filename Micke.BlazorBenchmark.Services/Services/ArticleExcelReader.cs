using Micke.BlazorBenchmark.WASM.Shared.Contracts;
using Micke.BlazorBenchmark.WASM.Shared.Entities;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Micke.BlazorBenchmark.Services.Services
{
  public class ArticleExcelReader : IExcelReader<Article>
  {
    public async Task<IList<Article>> ReadAsync(StreamReader stream)
    {
      var articles = new List<Article>();
      ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

      using(ExcelPackage package = new ExcelPackage())
      {
        await package.LoadAsync(stream.BaseStream);

        var worksheet = package.Workbook.Worksheets.FirstOrDefault();
        int colCount = worksheet.Dimension.End.Column;
        int rowCount = worksheet.Dimension.End.Row;

        for (int row = 1; row <= rowCount; row++)
        {
          if (row == 1) continue;

          try
          {
            articles.Add(GetArticle(worksheet, row, colCount));
          }
          catch (Exception)
          {}
        }
      }

      return articles;
     }

    private Article GetArticle(ExcelWorksheet worksheet, int row, int colCount)
    {
      var article = new Article();
      for (int col = 1; col <= colCount; col++)
      {
        article.Id = row-1;
        if (col == 12) article.Description = worksheet.Cells[row, col].Value.ToString();
        if (col == 2) article.SupplierId = worksheet.Cells[row, col].Value.ToString();
        if (col == 5) article.SubGroupId = Convert.ToInt32(worksheet.Cells[row, col].Value.ToString());
        if (col == 4) article.MainGroupId = Convert.ToInt32(worksheet.Cells[row, col].Value.ToString());
        if (col == 3) article.Price = Convert.ToDecimal(worksheet.Cells[row, col].Value.ToString());
        if (col == 7) article.Ean = worksheet.Cells[row, col].Value.ToString();
      }

      return article;
    }
  }
}
