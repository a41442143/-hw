using System.Text.Json;

string filePath = "App_Data/company.json";

if (!File.Exists(filePath))
{
    Console.WriteLine("找不到資料檔案，請確認 App_Data/company.json 是否存在！");
    return;
}

string json = File.ReadAllText(filePath);

List<CompanyStats>? data = JsonSerializer.Deserialize<List<CompanyStats>>(json);

Console.WriteLine("近月台灣公司統計資料");
Console.WriteLine("------------------------------------------------------");
Console.WriteLine("{0,-10} {1,8} {2,8} {3,10}",
    "月份", "上市家數", "上櫃家數", "未上市家數");
Console.WriteLine("------------------------------------------------------");

foreach (var item in data)
{
    Console.WriteLine("{0,-10} {1,8} {2,8} {3,10}",
        item.月別,
        item.上市公司_家數,
        item.上櫃公司_家數,
        item.未上市_家數);
}

Console.WriteLine("\n資料讀取完成！");
Console.ReadLine();
