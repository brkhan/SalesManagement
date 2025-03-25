using SalesManagement.Api.Services;
using SalesManagement.Domain.Services;

namespace SalesManagement.UI.Services;

public class SalesService(HttpClient httpClient)
{
    public async Task<List<SalesRecord>> GetTotalSalesAsync()
    {
        var sales = await httpClient.GetFromJsonAsync<List<SalesRecord>>("salesmanagement");
        return sales ?? [];
    }

    public async Task<List<SalesSummaryRecord>?> GetTotalSalesSummary(string? summaryType)
    {
        var sales = await httpClient.GetFromJsonAsync<List<SalesSummaryRecord>>($"salesmanagement/summary/{summaryType}");
        return sales ?? [];
    }
}
