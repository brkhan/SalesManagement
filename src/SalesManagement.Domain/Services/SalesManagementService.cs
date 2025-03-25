using System.Globalization;
using System.Text;
using CsvHelper;
using CsvHelper.Configuration;
using SalesManagement.Api.Services.CsvHelpers;
using SalesManagement.Domain.Services;
using SalesManagement.Domain.Services.CsvHelpers;

namespace SalesManagement.Api.Services
{
    public interface ISalesManagementService
    {
        List<SalesRecord> GetTotalSales();
        IEnumerable<SalesSummaryRecord> GetSummary(string type);
    }

    public class SalesManagementService : ISalesManagementService
    {
        private readonly string _filePath = Path.Combine(AppContext.BaseDirectory, "Data", "Data.csv");

        public List<SalesRecord> GetTotalSales()
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                TrimOptions = TrimOptions.Trim,
            };

            using var reader = new StreamReader(_filePath, Encoding.GetEncoding(1252));
            using var csv = new CsvReader(reader, config);
            csv.Context.RegisterClassMap<SaleRecordCsvMap>();
            var saleRecordCsvs = csv.GetRecords<SaleRecordCsv>().ToList();
            var mappedRecords = new List<SalesRecord>();
            foreach (var r in saleRecordCsvs)
            {
                try
                {
                    mappedRecords.Add(new SalesRecord()
                    {
                        Segment = r.Segment,
                        Country = r.Country,
                        Product = r.Product,
                        DiscountBand = r.DiscountBand,
                        UnitsSold = double.Parse(r.UnitsSold.TrimCsv()),
                        ManufacturingPrice = r.ManufacturingPrice,
                        ManufacturingPriceConverted = r.ManufacturingPrice.ParseCurrency(),
                        SalePrice = r.SalePrice,
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


        public IEnumerable<SalesSummaryRecord> GetSummary(string type)
        {
            var totalSales = GetTotalSales();

            return type.ToLower() switch
            {
                "country" => totalSales.GroupBy(s => s.Country)
                    .Select(g => new SalesSummaryRecord
                    {
                        SummaryKey = g.Key,
                        UnitsSold = g.Sum(s => s.UnitsSold),
                        ManufacturingPriceConverted = g.Sum(s => s.ManufacturingPriceConverted)
                    }),
                "product" => totalSales.GroupBy(s => s.Product)
                    .Select(g => new SalesSummaryRecord
                    {
                        SummaryKey = g.Key,
                        UnitsSold = g.Sum(s => s.UnitsSold),
                        ManufacturingPriceConverted = g.Sum(s => s.ManufacturingPriceConverted)
                    }),
                "segment" => totalSales.GroupBy(s => s.Segment)
                    .Select(g => new SalesSummaryRecord
                    {
                        SummaryKey = g.Key,
                        UnitsSold = g.Sum(s => s.UnitsSold),
                        ManufacturingPriceConverted = g.Sum(s => s.ManufacturingPriceConverted)
                    }),
                _ => throw new ArgumentException("Invalid summary type")
            };
        }
    }
}


public class SalesSummaryRecord
{
    public string SummaryKey { get; set; }
    public double UnitsSold { get; set; }
    public double ManufacturingPriceConverted { get; set; }
}

