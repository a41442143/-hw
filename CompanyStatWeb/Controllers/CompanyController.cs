using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using CompanyStatWeb.Models;
using CompanyStatWeb.Services;

namespace CompanyStatWeb.Controllers
{
    [Authorize]
    public class CompanyController : Controller
    {
        private readonly CompanyDataService _service;

        public CompanyController(CompanyDataService service)
        {
            _service = service;
        }

        public IActionResult Index(string sortOrder, string searchString)
        {
            ViewData["CurrentFilter"] = searchString;
            ViewData["MonthSortParm"] = String.IsNullOrEmpty(sortOrder) ? "month_desc" : "";
            ViewData["CountSortParm"] = sortOrder == "Count" ? "count_desc" : "Count";
            ViewData["CapitalSortParm"] = sortOrder == "Capital" ? "capital_desc" : "Capital";
            ViewData["MarketValueSortParm"] = sortOrder == "MarketValue" ? "marketValue_desc" : "MarketValue";

            var data = _service.GetAll().AsEnumerable();

            if (!String.IsNullOrEmpty(searchString))
            {
                data = data.Where(s => s.Month.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "month_desc":
                    data = data.OrderByDescending(s => s.Month);
                    break;
                case "Count":
                    data = data.OrderBy(s => Convert.ToDouble(s.PublicCompanyCount));
                    break;
                case "count_desc":
                    data = data.OrderByDescending(s => Convert.ToDouble(s.PublicCompanyCount));
                    break;
                case "Capital":
                    data = data.OrderBy(s => Convert.ToDouble(s.PublicCompanyCapital));
                    break;
                case "capital_desc":
                    data = data.OrderByDescending(s => Convert.ToDouble(s.PublicCompanyCapital));
                    break;
                case "MarketValue":
                    data = data.OrderBy(s => Convert.ToDouble(s.PublicCompanyMarketValue));
                    break;
                case "marketValue_desc":
                    data = data.OrderByDescending(s => Convert.ToDouble(s.PublicCompanyMarketValue));
                    break;
                default:
                    data = data.OrderBy(s => s.Month);
                    break;
            }

            return View(data.ToList());
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult Create(CompanyStats company)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _service.Add(company);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return View(company);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var company = _service.GetById(id);
            if (company == null)
            {
                return NotFound();
            }
            return View(company);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(string id, CompanyStats company)
        {
            if (id != company.Month)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _service.Update(company);
                return RedirectToAction(nameof(Index));
            }
            return View(company);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var company = _service.GetById(id);
            if (company == null)
            {
                return NotFound();
            }

            return View(company);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteConfirmed(string id)
        {
            _service.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}