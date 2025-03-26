using SalesManagement.Domain.Services;
using SalesManagement.Domain.Services.Models;

namespace SalesManagement.UI.Services;

public class SalesViewService(ISalesManagementService salesManagementService) : ISalesViewService
{
    public async Task<IList<SalesItem>> GetTotalSalesAsync()
    {
        var sales = salesManagementService.GetTotalSales();
        return await Task.FromResult(sales ?? []);
    }

    public async Task<IList<SalesSummaryItem>> GetTotalSalesSummary(string? summaryType)
    {
        var sales = salesManagementService.GetSummary(summaryType);
        return await Task.FromResult(sales.ToList() ?? []);
    }
}
