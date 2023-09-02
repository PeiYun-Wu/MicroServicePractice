using Mango.Web.Models;
using Mango.Web.Services.IServices;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace Mango.Web.Services
{//實現接口
    public class BaseService : IBaseService
    {
        private bool disposedValue;

        public ResponseDto responseModel { get ; set ; }
        public IHttpClientFactory httpClient { get; set ; }

        public BaseService(IHttpClientFactory httpClient) 
        {
            this.responseModel = new ResponseDto();
            this.httpClient = httpClient;
        }
        public async Task<T> SendAsync<T>(ApiRequest apiRequest)
        {
            try {
                var client = httpClient.CreateClient("MangoAPI");
                HttpRequestMessage message = new HttpRequestMessage();
                message.Headers.Add("Accept", "application/json"); //開始進行配置
                message.RequestUri=new Uri(apiRequest.Url);
                client.DefaultRequestHeaders.Clear(); //清除客戶端默認的請求(?)
                if(apiRequest.Data != null) //檢查數據
                {
                    message.Content = new StringContent(JsonConvert.SerializeObject(apiRequest.Data),
                       Encoding.UTF8, "application/json");
                }

                if (!string.IsNullOrEmpty(apiRequest.AccessToken))
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiRequest.AccessToken);
                }

                HttpResponseMessage apiResponse = null;
                switch (apiRequest.ApiType)
                {
                    case SD.ApiType.POST:
                        message.Method = HttpMethod.Post;
                        break;
                    case SD.ApiType.PUT:
                        message.Method = HttpMethod.Put;
                        break;
                    case SD.ApiType.DELETE:
                        message.Method = HttpMethod.Delete;
                        break;
                    default:
                        message.Method = HttpMethod.Get;
                        break;

                }
                apiResponse = await client.SendAsync(message); //可在這裡設置中斷點debug
                var apiContent = await apiResponse.Content.ReadAsStringAsync();
                var apiResponseDto = JsonConvert.DeserializeObject<T>(apiContent);  //<T>
                return apiResponseDto;
            }
            catch (Exception e)
            {
                var dto = new ResponseDto
                {
                    DisplayMessage = "Error",
                    ErrorMessages = new List<string> { Convert.ToString(e.Message) },
                    IsSuccess = false
                };
                var res = JsonConvert.SerializeObject(dto);
                var apiResponseDto = JsonConvert.DeserializeObject<T>(res);
                return apiResponseDto;
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: 處置受控狀態 (受控物件)
                }

                // TODO: 釋出非受控資源 (非受控物件) 並覆寫完成項
                // TODO: 將大型欄位設為 Null
                disposedValue = true;
            }
        }

        // // TODO: 僅有當 'Dispose(bool disposing)' 具有會釋出非受控資源的程式碼時，才覆寫完成項
        // ~BaseService()
        // {
        //     // 請勿變更此程式碼。請將清除程式碼放入 'Dispose(bool disposing)' 方法
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // 請勿變更此程式碼。請將清除程式碼放入 'Dispose(bool disposing)' 方法
           // Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
