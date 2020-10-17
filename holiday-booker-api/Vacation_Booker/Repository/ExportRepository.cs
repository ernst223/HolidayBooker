using OfficeOpenXml;
using System;
using System.IO;
using System.Linq;
using Vacation_Booker.Entities;
using Vacation_Booker.Models;

namespace Vacation_Booker.Repository
{
    public class ExportRepository
    {
        private MyContext dc;
        private VacationRepository vacationRepository;
        public ExportRepository(MyContext T)
        {
            dc = T;
            vacationRepository = new VacationRepository(T);
        }

        public Stream GenerateReport(FilterStock T)
        {
            var data = vacationRepository.getFilteredVacation(T).Where(o => o.Sold == false).ToList();
            var fileName = DateTime.Now.Ticks + ".xlsx";
            FileInfo file = new FileInfo(fileName);
            FileInfo templateFile = new FileInfo(@"Templates/ReportingTemplate.xlsx");
            ExcelPackage package = new ExcelPackage(file, templateFile);
            ExcelWorksheet worksheet = package.Workbook.Worksheets.FirstOrDefault();

            worksheet.Cells[1, 1].Value = string.Format("Holiday Booker Stock");
            worksheet.Cells[2, 1].Value = "Resort";
            worksheet.Cells[2, 2].Value = "Unit Size";
            worksheet.Cells[2, 3].Value = "Region";
            worksheet.Cells[2, 4].Value = "Area";
            worksheet.Cells[2, 5].Value = "Arrival";
            worksheet.Cells[2, 6].Value = "Nights";
            worksheet.Cells[2, 7].Value = "Buying Price";
            worksheet.Cells[2, 8].Value = "Price";
            worksheet.Cells[2, 9].Value = "Profit";
            worksheet.Cells[2, 10].Value = "Provider";
            worksheet.Cells[2, 11].Value = "Admin Fee";
            int row = 3;
            int? regionId;
            int? areaId;
            foreach (var d in data)
            {
                worksheet.Cells[row, 1].Value = d.Resort;
                worksheet.Cells[row, 2].Value = d.UnitSize;
                try
                {
                    regionId = dc.Resorts.Where(o => o.Id == d.ResortId).FirstOrDefault().RegionId;
                    areaId = dc.Regions.Where(o => o.Id == regionId).FirstOrDefault().AreaId;
                    if (regionId != null)
                    {
                        worksheet.Cells[row, 3].Value = dc.Areas.Where(o => o.Id == areaId).First().Description;
                        worksheet.Cells[row, 4].Value = dc.Regions.Where(o => o.Id == regionId).First().Description;
                    }
                }
                catch
                {
                }
                
                worksheet.Cells[row, 5].Value = Convert.ToDateTime(d.Arrival).ToString("dd MMM yyy");
                worksheet.Cells[row, 6].Value = d.Nights;
                worksheet.Cells[row, 7].Value = d.BuyingPrice;
                worksheet.Cells[row, 8].Value = d.Price2Pay;
                worksheet.Cells[row, 9].Value = d.Price2Pay - d.BuyingPrice;
                worksheet.Cells[row, 10].Value = d.Provider;
                worksheet.Cells[row, 11].Value = d.AdminFee;
                row++;
            }

            //Add values
            var dataBytes = package.GetAsByteArray();
            Stream dataStream = new MemoryStream(dataBytes);
            dataStream.Seek(0, SeekOrigin.Begin);
            return dataStream;
        }
    }
}
