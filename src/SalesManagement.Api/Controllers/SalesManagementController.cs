using Microsoft.AspNetCore.Mvc;
using SalesManagement.Domain.Services;
using SalesManagement.Domain.Services.Models;


namespace SalesManagement.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class SalesManagementController(
    ILogger<SalesManagementController> logger,
    ISalesManagementService salesManagementService)
    : ControllerBase
{
    [HttpGet(Name = "GetTotalSales")]
    public ActionResult<IEnumerable<SalesItem>> Get()
    {
        return new OkObjectResult(salesManagementService.GetTotalSales());
    }

    [HttpGet("summary/{type}", Name = "GetByTypeSummary")]
    public ActionResult<IEnumerable<SalesSummaryItem>> GetSummary(string type)
    {
        return new OkObjectResult(salesManagementService.GetSummary(type));
    }
}