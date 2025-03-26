using CsvHelper.Configuration;

namespace SalesManagement.Domain.Services.CsvHelpers;

public sealed class SaleRecordCsvMap : ClassMap<SaleRecordCsv>
{
    public SaleRecordCsvMap()
    {
        Map(m => m.Segment).Name("Segment");
        Map(m => m.Country).Name("Country");
        Map(m => m.Product).Name("Product");
        Map(m => m.DiscountBand).Name("Discount Band");
        Map(m => m.UnitsSold).Name("Units Sold");
        Map(m => m.ManufacturingPrice).Name("Manufacturing Price");
        Map(m => m.SalePrice).Name("Sale Price");
        Map(m => m.Date).Name("Date");
    }
}