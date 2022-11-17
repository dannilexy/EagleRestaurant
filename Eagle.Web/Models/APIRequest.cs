using static Eagle.Web.SD;

namespace Eagle.Web.Models
{
    public class APIRequest
    {
        public ApiType ApiType { get; set; } = ApiType.Get;
        public string Url { get; set; }
        public object Data { get; set; }
        public string Token { get; set; } = string.Empty;
    }
}
