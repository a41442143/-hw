namespace CompanyStatWeb.ViewModels;

public class DashboardViewModel
{
    public int TotalRecords { get; set; }
    public decimal TotalCapital { get; set; }
    public decimal TotalMarketValue { get; set; }
    public double AverageCompanyCount { get; set; }
    public string LatestMonth { get; set; } = string.Empty;
}