using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;
using SalesManagement.Api.Services;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using SalesManagement.Domain.Services;

namespace SalesManagement.Api.UnitTests;

public class SalesManagementControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;
    private readonly HttpClient _client;
    private readonly Mock<ISalesManagementService> _salesManagementServiceMock;

    public SalesManagementControllerTests(WebApplicationFactory<Program> factory)
    {
        _salesManagementServiceMock = new Mock<ISalesManagementService>();

        _factory = factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureServices(services =>
            {
                services.RemoveAll(typeof(ISalesManagementService));
                services.AddSingleton(_salesManagementServiceMock.Object);
            });
        });

        _client = _factory.CreateClient();
    }

    [Fact]
    public async Task Get_ShouldReturnTotalSales()
    {
        // Arrange
        var expectedSales = new List<SalesRecord>
        {
            new SalesRecord { Country = "USA", Product = "Product1", UnitsSold = 100 },
            new SalesRecord { Country = "Canada", Product = "Product2", UnitsSold = 200 }
        };
        _salesManagementServiceMock.Setup(service => service.GetTotalSales()).Returns(expectedSales);

        // Act
        var response = await _client.GetAsync("/SalesManagement");
        response.EnsureSuccessStatusCode();

        var sales = await response.Content.ReadFromJsonAsync<List<SalesRecord>>();

        // Assert
        Assert.NotNull(sales);
        Assert.Equal(2, sales.Count);
    }

    [Fact]
    public async Task GetSummary_ShouldReturnSummaryByType()
    {
        // Arrange
        var summaryType = "country";
        var expectedSummary = new List<SalesSummaryRecord>
        {
            new SalesSummaryRecord { SummaryKey = "USA", UnitsSold = 1000, ManufacturingPriceConverted = 5000 },
            new SalesSummaryRecord { SummaryKey = "Canada", UnitsSold = 2000, ManufacturingPriceConverted = 10000 }
        };
        _salesManagementServiceMock.Setup(service => service.GetSummary(summaryType)).Returns(expectedSummary);

        // Act
        var response = await _client.GetAsync($"/SalesManagement/summary/{summaryType}");
        response.EnsureSuccessStatusCode();

        var summary = await response.Content.ReadFromJsonAsync<List<SalesSummaryRecord>>();

        // Assert
        Assert.NotNull(summary);
        Assert.Equal(expectedSummary.Count, summary.Count);
    }
}