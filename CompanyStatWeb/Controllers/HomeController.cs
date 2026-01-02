using Microsoft.AspNetCore.Mvc;
using CompanyStatWeb.Models;
using CompanyStatWeb.Services;
using CompanyStatWeb.ViewModels;
using System.Diagnostics;

namespace CompanyStatWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly CompanyDataService _service;

        public HomeController(ILogger<HomeController> logger, CompanyDataService service)
        {
            _logger = logger;
            _service = service;
        }

        public IActionResult Index()
        {
            var data = _service.GetAll();
            
            // Calculate Dashboard Stats
            var viewModel = new DashboardViewModel
            {
                TotalRecords = data.Count,
                TotalCapital = data.Sum(x => decimal.TryParse(x.PublicCompanyCapital, out var c) ? c : 0),
                TotalMarketValue = data.Sum(x => decimal.TryParse(x.PublicCompanyMarketValue, out var m) ? m : 0),
                AverageCompanyCount = data.Count > 0 ? data.Average(x => double.TryParse(x.PublicCompanyCount, out var c) ? c : 0) : 0,
                LatestMonth = data.OrderByDescending(x => x.Month).FirstOrDefault()?.Month ?? "N/A"
            };

            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}