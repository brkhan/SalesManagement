using SalesManagement.Domain.Services.Models;

namespace SalesManagement.Domain.Services;

public interface ISalesManagementService
{
    List<SalesItem> GetTotalSales();
    IEnumerable<SalesSummaryItem> GetSummary(string type);
}