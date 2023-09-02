using Mango.Web.Models;

namespace Mango.Web.Services.IServices
{//公共接口
    public interface IBaseService  :IDisposable
    {
        //做個響應DTO 接收get;set;
        ResponseDto responseModel { get; set; }
        //使用泛型,儲存或發送 與api相關請求的所有內容
        Task<T> SendAsync<T>(ApiRequest apiRequest);

    }
}
