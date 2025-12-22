using System.Text.Json;
using CompanyStatWeb.Models;

namespace CompanyStatWeb.Services
{
    public class CompanyDataService
    {
        private readonly string _filePath;

        public CompanyDataService(IWebHostEnvironment webHostEnvironment)
        {
            // 設定 JSON 檔案的路徑
            string contentRootPath = webHostEnvironment.ContentRootPath;
            _filePath = Path.Combine(contentRootPath, "App_Data", "company.json");
        }

        public List<CompanyStats> GetAll()
        {
            if (!File.Exists(_filePath))
            {
                return new List<CompanyStats>();
            }

            var json = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<CompanyStats>>(json) ?? new List<CompanyStats>();
        }

        public CompanyStats? GetById(string month)
        {
            var list = GetAll();
            return list.FirstOrDefault(c => c.Month == month);
        }

        public void Add(CompanyStats company)
        {
            var list = GetAll();
            // 簡單檢查是否存在
            if (list.Any(c => c.Month == company.Month))
            {
                throw new Exception($"Month {company.Month} already exists.");
            }
            list.Add(company);
            Save(list);
        }

        public void Update(CompanyStats company)
        {
            var list = GetAll();
            var existingIndex = list.FindIndex(c => c.Month == company.Month);
            if (existingIndex != -1)
            {
                list[existingIndex] = company;
                Save(list);
            }
        }

        public void Delete(string month)
        {
            var list = GetAll();
            var item = list.FirstOrDefault(c => c.Month == month);
            if (item != null)
            {
                list.Remove(item);
                Save(list);
            }
        }

        private void Save(List<CompanyStats> list)
        {
            var options = new JsonSerializerOptions { WriteIndented = true, Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping };
            var json = JsonSerializer.Serialize(list, options);
            File.WriteAllText(_filePath, json);
        }
    }
}
