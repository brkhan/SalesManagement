namespace SalesManagement.Domain.Services.Models;

public record SalesItem
{
    public string Segment { get; set; }
    public string Country { get; set; }
    public string Product { get; set; }
    public string DiscountBand { get; set; }
    public double UnitsSold { get; set; }
    public string ManufacturingPrice { get; set; }
    public double ManufacturingPriceConverted { get; set; }
    public string SalesPrice { get; set; }
    public DateTime Date { get; set; }
    public double SalesPriceConverted { get; set; }
}