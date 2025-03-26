using System.Text;
using SalesManagement.Domain.Services;
using SalesManagement.Domain.Services.Models;

namespace SalesManagement.Api.UnitTests
{
    public class SalesManagementServiceTests
    {
        public SalesManagementServiceTests()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }

        [Fact]
        public void GetTotalSales_ShouldReturnRecords()
        {
            // Arrange
            var service = new SalesManagementService();
   
            // Act
            List<SalesItem> result = service.GetTotalSales();

            // Assert
            Assert.NotEmpty(result);
            // 2 records are considered invalid and ignored
            Assert.True(result.Count == 698);
        }


        [Fact]
        public void GetTotalSales_Should_Parse_Records()
        {
            // Arrange
            var service = new SalesManagementService();

            // Act
            List<SalesItem> result = service.GetTotalSales();

            // Assert
            var sortedResults = result.OrderBy(x => x.Country).ThenBy(z => z.Product).ToList();
            Assert.Equal("Canada", sortedResults[0].Country);
            Assert.Equal("01/12/2014", sortedResults[0].Date.ToString("d"));
            Assert.Equal("Low", sortedResults[0].DiscountBand);
            Assert.Equal("£260.00", sortedResults[0].ManufacturingPrice);
            Assert.Equal("Amarilla", sortedResults[0].Product);
            Assert.Equal("£300.00", sortedResults[0].SalesPrice);
            Assert.Equal("Small Business", sortedResults[0].Segment);
            Assert.Equal("1916", sortedResults[0].UnitsSold.ToString());
     
        }

        [Fact]
        public void GetSalesSuumary_ShouldReturn_Summary_ForCountries()
        {
            // Arrange
            var service = new SalesManagementService();

            // Act
            List<SalesSummaryItem> result = service.GetSummary("country").ToList();

            // Assert
            Assert.NotEmpty(result);
            Assert.True(result.Select(rs => rs.SummaryKey).Contains("Canada"));
            Assert.True(result.Select(rs => rs.SummaryKey).Contains("Mexico"));
            Assert.False(result.Select(rs => rs.SummaryKey).Contains("Small Business"));
        }

        [Fact]
        public void GetSalesSuumary_ShouldReturn_Summary_ForSegments()
        {
            // Arrange
            var service = new SalesManagementService();

            // Act
            List<SalesSummaryItem> result = service.GetSummary("segment").ToList();

            // Assert
            Assert.NotEmpty(result);
            Assert.False(result.Select(rs => rs.SummaryKey).Contains("Canada"));
            Assert.False(result.Select(rs => rs.SummaryKey).Contains("Mexico"));
            Assert.True(result.Select(rs => rs.SummaryKey).Contains("Small Business"));
        }

        [Fact]
        public void GetSalesSummary_ShouldReturn_Totals_ForProducts()
        {
            // Arrange
            var service = new SalesManagementService();

            // Act
            List<SalesSummaryItem> result = service.GetSummary("Product").ToList();

            // Assert
            Assert.NotEmpty(result);
            var testRecord = result.FirstOrDefault(a => a.SummaryKey == "Amarilla");
            Assert.NotNull(testRecord);
            Assert.Equal(24440,testRecord.ManufacturingPriceConverted);
            Assert.Equal(12096, testRecord.SalesPriceConverted);
            Assert.Equal(155315, testRecord.UnitsSold);
        }
    }
}

