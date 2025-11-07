#nullable disable
using System.Text.Json.Serialization;

public class CompanyStats
{
    public string 月別 { get; set; }

    [JsonPropertyName("上市公司-家數")]
    public string 上市公司_家數 { get; set; }

    [JsonPropertyName("上櫃公司-家數")]
    public string 上櫃公司_家數 { get; set; }

    [JsonPropertyName("未上市未上櫃公司-家數")]
    public string 未上市_家數 { get; set; }
}
