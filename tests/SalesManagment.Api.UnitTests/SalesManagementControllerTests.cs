using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using SalesManagement.Domain.Services;
using SalesManagement.Domain.Services.Models;

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
        var expectedSales = new List<SalesItem>
        {
            new SalesItem { Country = "USA", Product = "Product1", UnitsSold = 100 },
            new SalesItem { Country = "Canada", Product = "Product2", UnitsSold = 200 }
        };
        _salesManagementServiceMock.Setup(service => service.GetTotalSales()).Returns(expectedSales);

        // Act
        var response = await _client.GetAsync("/SalesManagement");
        response.EnsureSuccessStatusCode();

        var sales = await response.Content.ReadFromJsonAsync<List<SalesItem>>();

        // Assert
        Assert.NotNull(sales);
        Assert.Equal(2, sales.Count);
    }

    [Fact]
    public async Task GetSummary_ShouldReturnSummaryByType()
    {
        // Arrange
        var summaryType = "country";
        var expectedSummary = new List<SalesSummaryItem>
        {
            new SalesSummaryItem { SummaryKey = "USA", UnitsSold = 1000, ManufacturingPriceConverted = 5000 },
            new SalesSummaryItem { SummaryKey = "Canada", UnitsSold = 2000, ManufacturingPriceConverted = 10000 }
        };
        _salesManagementServiceMock.Setup(service => service.GetSummary(summaryType)).Returns(expectedSummary);

        // Act
        var response = await _client.GetAsync($"/SalesManagement/summary/{summaryType}");
        response.EnsureSuccessStatusCode();

        var summary = await response.Content.ReadFromJsonAsync<List<SalesSummaryItem>>();

        // Assert
        Assert.NotNull(summary);
        Assert.Equal(expectedSummary.Count, summary.Count);
    }
}