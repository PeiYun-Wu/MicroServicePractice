using static Mango.Web.SD;

namespace Mango.Web.Models
{//保存api請求,好發送給任何服務(services eg.ProductAPI)
    public class ApiRequest
    {
        public ApiType ApiType { get; set; } = ApiType.GET;  //默認類型為get
        public string Url { get; set; }
        public object Data { get; set; }  //需要儲存任何東西 且也不確定類型是甚麼 先設成泛型 所以用object
        public string AccessToken { get; set; }
    }
}
