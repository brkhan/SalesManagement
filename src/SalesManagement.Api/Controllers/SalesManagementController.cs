using Microsoft.AspNetCore.Mvc;
using SalesManagement.Api.Services;
using SalesManagement.Domain.Services;


namespace SalesManagement.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class SalesManagementController(
    ILogger<SalesManagementController> logger,
    ISalesManagementService salesManagementService)
    : ControllerBase
{
    private readonly ILogger<SalesManagementController> _logger = logger;

    [HttpGet(Name = "GetTotalSales")]
    public IEnumerable<SalesRecord> Get()
    {
        return salesManagementService.GetTotalSales();
    }

    [HttpGet("summary/{type}", Name = "GetByTypeSummary")]
    public IEnumerable<SalesSummaryRecord> GetSummary(string type)
    {
        return salesManagementService.GetSummary(type);
    }
}