using System.Globalization;
using System.Text;
using CsvHelper;
using CsvHelper.Configuration;
using SalesManagement.Domain.Services.CsvHelpers;
using SalesManagement.Domain.Services.Models;
using Ude;

namespace SalesManagement.Domain.Services
{
    public class SalesManagementService : ISalesManagementService
    {
        private readonly string _filePath = Path.Combine(AppContext.BaseDirectory, "Data", "Data.csv");

        // the file encoding type can be made configurable
        private static readonly Encoding FileEncoding = Encoding.GetEncoding(1252);

        public List<SalesItem> GetTotalSales()
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                TrimOptions = TrimOptions.Trim,
            };

            using var reader = new StreamReader(_filePath, FileEncoding);
            using var csv = new CsvReader(reader, config);
            csv.Context.RegisterClassMap<SaleRecordCsvMap>();
            var saleRecordCsvs = csv.GetRecords<SaleRecordCsv>().ToList();
            var mappedRecords = new List<SalesItem>();
            foreach (var r in saleRecordCsvs)
            {
                try
                {
                    mappedRecords.Add(new SalesItem()
                    {
                        Segment = r.Segment,
                        Country = r.Country,
                        Product = r.Product,
                        DiscountBand = r.DiscountBand,
                        UnitsSold = double.Parse(r.UnitsSold.TrimCsv()),
                        ManufacturingPrice = r.ManufacturingPrice,
                        ManufacturingPriceConverted = r.ManufacturingPrice.ParseCurrency(),
                        SalesPrice = r.SalePrice,
                        SalesPriceConverted = r.SalePrice.ParseCurrency(),
                        Date = DateTime.ParseExact(r.Date.TrimCsv(), "dd/MM/yyyy", CultureInfo.InvariantCulture)
                    });
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }
            return mappedRecords;
        }

        public IEnumerable<SalesSummaryItem> GetSummary(string type)
        {
            var totalSales = GetTotalSales();

            return type.ToLower() switch
            {
                "country" => totalSales.GroupBy(s => s.Country)
                    .Select(g => new SalesSummaryItem
                    {
                        SummaryKey = g.Key,
                        UnitsSold = g.Sum(s => s.UnitsSold),
                        ManufacturingPriceConverted = g.Sum(s => s.ManufacturingPriceConverted),
                        SalesPriceConverted = g.Sum(s => s.SalesPriceConverted)
                    }),
                "product" => totalSales.GroupBy(s => s.Product)
                    .Select(g => new SalesSummaryItem
                    {
                        SummaryKey = g.Key,
                        UnitsSold = g.Sum(s => s.UnitsSold),
                        ManufacturingPriceConverted = g.Sum(s => s.ManufacturingPriceConverted),
                        SalesPriceConverted = g.Sum(s => s.SalesPriceConverted)

                    }),
                "segment" => totalSales.GroupBy(s => s.Segment)
                    .Select(g => new SalesSummaryItem
                    {
                        SummaryKey = g.Key,
                        UnitsSold = g.Sum(s => s.UnitsSold),
                        ManufacturingPriceConverted = g.Sum(s => s.ManufacturingPriceConverted),
                        SalesPriceConverted = g.Sum(s => s.SalesPriceConverted)
                    }),
                _ => new List<SalesSummaryItem>()
                    {
                        new()
                        {
                            SummaryKey = "Total sales",
                            UnitsSold = totalSales.Sum(s => s.UnitsSold),
                            ManufacturingPriceConverted = totalSales.Sum(s => s.ManufacturingPriceConverted),
                            SalesPriceConverted = totalSales.Sum(s => s.SalesPriceConverted)
                        }
                    }
            };
        }
        //
        // private Encoding DetectFileEncoding(string filePath)
        // {
        //     try
        //     {
        //         using var fileStream = File.OpenRead(filePath);
        //         var detector = new CharsetDetector();
        //         detector.Feed(fileStream);
        //         detector.DataEnd();
        //         return Encoding.GetEncoding(detector.Charset ?? "windows-1252");
        //     }
        //     catch (Exception ex)
        //     {
        //         throw ex;
        //     }
        // }
    }
}