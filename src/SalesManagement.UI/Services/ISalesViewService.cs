using SalesManagement.Domain.Services.Models;

namespace SalesManagement.UI.Services;

public interface ISalesViewService
{
    Task<IList<SalesItem>> GetTotalSalesAsync();
    Task<IList<SalesSummaryItem>> GetTotalSalesSummary(string? summaryType);
}