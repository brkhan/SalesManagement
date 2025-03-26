namespace SalesManagement.Domain.Services.Models;

public record SalesSummaryItem
{
    public string SummaryKey { get; set; }
    public double UnitsSold { get; set; }
    public double ManufacturingPriceConverted { get; set; }
    public double SalesPriceConverted { get; set; }
}