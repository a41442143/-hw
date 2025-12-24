using System.ComponentModel.DataAnnotations;

namespace CompanyStatWeb.ViewModels;

public class SiteSettingsViewModel
{
    [Required]
    [Display(Name = "Site Name")]
    public string SiteName { get; set; } = "CompanyStat Pro";

    [Required]
    [EmailAddress]
    [Display(Name = "Contact Email")]
    public string ContactEmail { get; set; } = "admin@company.com";

    [Display(Name = "Items Per Page")]
    public int ItemsPerPage { get; set; } = 20;

    [Display(Name = "Enable Maintenance Mode")]
    public bool MaintenanceMode { get; set; } = false;
}
