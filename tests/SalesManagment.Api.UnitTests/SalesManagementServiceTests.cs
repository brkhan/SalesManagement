using System.Text;
using SalesManagement.Api.Services;
using SalesManagement.Domain.Services;

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
            List<SalesRecord> result = service.GetTotalSales();

            // Assert
            Assert.NotEmpty(result);
            Assert.True(result.Count == 698);
        }


        [Fact]
        public void GetTotalSales_Should_Parse_Records()
        {
            // Arrange
            var service = new SalesManagementService();

            // Act
            List<SalesRecord> result = service.GetTotalSales();

            // Assert
            var sortedResults = result.OrderBy(x => x.Country).ThenBy(z => z.Product).ToList();
            Assert.Equal("Canada", sortedResults[0].Country);
            Assert.Equal("01/12/2014", sortedResults[0].Date.ToString("d"));
            Assert.Equal("Low", sortedResults[0].DiscountBand);
            Assert.Equal("£260.00", sortedResults[0].ManufacturingPrice);
            Assert.Equal("Amarilla", sortedResults[0].Product);
            Assert.Equal("£300.00", sortedResults[0].SalePrice);
            Assert.Equal("Small Business", sortedResults[0].Segment);
            Assert.Equal("1916", sortedResults[0].UnitsSold.ToString());
     
        }
    }
}

