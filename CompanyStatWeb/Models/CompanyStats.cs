using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CompanyStatWeb.Models
{
    public class CompanyStats
    {
        [Key]
        [Display(Name = "月別")]
        [JsonPropertyName("月別")]
        public string Month { get; set; } = string.Empty;

        [Display(Name = "上市公司家數")]
        [JsonPropertyName("上市公司-家數")]
        public string PublicCompanyCount { get; set; } = string.Empty;

        [Display(Name = "上市公司資本額")]
        [JsonPropertyName("上市公司-資本額（金額）")]
        public string PublicCompanyCapital { get; set; } = string.Empty;

        [Display(Name = "上市公司成長率")]
        [JsonPropertyName("上市公司-成長率")]
        public string PublicCompanyGrowthRate { get; set; } = string.Empty;

        [Display(Name = "上市公司面值")]
        [JsonPropertyName("上市公司-上市面值（金額）")]
        public string PublicCompanyFaceValue { get; set; } = string.Empty;

        [Display(Name = "上市公司市值")]
        [JsonPropertyName("上市公司-上市公司市值（金額）")]
        public string PublicCompanyMarketValue { get; set; } = string.Empty;

        [Display(Name = "上櫃公司家數")]
        [JsonPropertyName("上櫃公司-家數")]
        public string OtcCompanyCount { get; set; } = string.Empty;

        [Display(Name = "上櫃公司資本額")]
        [JsonPropertyName("上櫃公司-資本額（金額）")]
        public string OtcCompanyCapital { get; set; } = string.Empty;

        [Display(Name = "上櫃公司成長率")]
        [JsonPropertyName("上櫃公司-成長率")]
        public string OtcCompanyGrowthRate { get; set; } = string.Empty;

        [Display(Name = "上櫃公司面值")]
        [JsonPropertyName("上櫃公司-上櫃面值（金額）")]
        public string OtcCompanyFaceValue { get; set; } = string.Empty;

        [Display(Name = "上櫃公司市值")]
        [JsonPropertyName("上櫃公司-上櫃市值（金額）")]
        public string OtcCompanyMarketValue { get; set; } = string.Empty;

        [Display(Name = "未上市櫃公司家數")]
        [JsonPropertyName("未上市未上櫃公司-家數")]
        public string NonPublicCompanyCount { get; set; } = string.Empty;

        [Display(Name = "未上市櫃公司資本額")]
        [JsonPropertyName("未上市未上櫃公司-資本額（金額）")]
        public string NonPublicCompanyCapital { get; set; } = string.Empty;
    }
}
