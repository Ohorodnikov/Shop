using OfficeOpenXml;

using Shop.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Shop.Infrastructure
{
    public class Excel
    {
        private const string XlsxContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        ExcelPackage excel;
        public Excel(Stream stream)
        {
            excel = new ExcelPackage(stream);
        }

        public Excel(string fileName)
        {
            var path = $@"./documents";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            File.Create(Path.Combine(path, $"{fileName}.xlsx"));
            MemoryStream stream = new MemoryStream();
            using (var fileStream = new FileStream(Path.Combine(path, $"{fileName}.docx"), FileMode.OpenOrCreate, FileAccess.Read))
            {
                fileStream.CopyTo(stream);                
            }
            excel = new ExcelPackage();
        }

        public static void WriteData(List<PurchaseViewModel> purchases, string name)
        {
            if (purchases == null)
            {
                throw new ArgumentNullException(nameof(purchases));
            }
            var excel = new ExcelPackage();
            excel.Workbook.Worksheets.Add("Purchases");
            var page = excel.Workbook.Worksheets.First();
            page.Cells[1, 1].Value = "ID";
            page.Cells[1, 2].Value = "Username(id)";
            page.Cells[1, 3].Value = "Total Price";
            page.Cells[1, 4].Value = "Date and Time";
            for (int i = 0; i < purchases.Count(); i++)
            {
                var p = purchases.ElementAt(i);
                page.Cells[i + 2, 1].Value = $"{p.Id}";
                page.Cells[i + 2, 2].Value = $"{p.User.UserName}({p.User.Id})";
                page.Cells[i + 2, 3].Value = $"{p.TotalPrice}";
                page.Cells[i + 2, 4].Value = $"{p.DateTime}";
            }
            excel.SaveAs(new FileInfo($@"./wwwroot/Documents/{name}.xlsx"));
        }

    }
}
