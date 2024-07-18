using Microsoft.EntityFrameworkCore;
using ODataEpplus.Models;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ODataEpplus.Context
{
    public class DataContext : DbContext
    {
        public DbSet<CRM> CRMs { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            LoadExcelData();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        private void LoadExcelData()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Xlsx", "crm_data.xlsx");

            Console.WriteLine($"File path: {filePath}");

            using var package = new ExcelPackage(new FileInfo(filePath));
            var worksheet = package.Workbook.Worksheets[0];
            var rowCount = worksheet.Dimension.Rows;

            var crmDataList = new List<CRM>();
            for (int row = 2; row <= rowCount; row++)
            {
                var crm = new CRM
                {
                    Id = int.Parse(worksheet.Cells[row, 1].Value.ToString()),
                    Customer = worksheet.Cells[row, 2].Value.ToString(),
                    Contact = worksheet.Cells[row, 3].Value.ToString(),
                    Email = worksheet.Cells[row, 4].Value.ToString()
                };
                crmDataList.Add(crm);
            }

            CRMs.RemoveRange(CRMs);
            CRMs.AddRange(crmDataList);
            SaveChanges();
        }
    }
}
