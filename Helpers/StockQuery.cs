using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace api.Helpers
{
    public enum SortByOptions
    {
        None,
        CompanyName,
        Symbol,
        Price
    }

    public class StockQuery
    {
        public string? Symbol { get; set; } = null;
        public string? CompanyName { get; set; } = null;
        [JsonConverter(typeof(StringEnumConverter))]
        public SortByOptions? SortBy { get; set; } = SortByOptions.None;
        public bool IsDecsending { get; set; } = false;
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20;
    }
}