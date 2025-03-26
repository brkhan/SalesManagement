using System.Globalization;

namespace SalesManagement.Domain.Services.CsvHelpers;

public static class CsvStringExtensions
{
    public static string TrimCsv(this string str, string oldChar = " ", string newChar = "")
    {
        return str.Replace(oldChar, newChar);
    }

    public static double ParseCurrency(this string input)
    {
        // Remove the currency symbol and any extra spaces
        var cleanedInput = input.Replace("£", "").TrimCsv();
        // Parse the cleaned string to a double
        return double.Parse(cleanedInput, CultureInfo.InvariantCulture);
    }

}