using Client.MVVM.Model;
using Client.MVVM.Model.DTO;
using Client.MVVM.Model.Utilities;
using Microsoft.Extensions.Configuration;

namespace Client.Services
{
    public class TravelService : BaseService
    {
        private readonly IHttpClientFactory clientFactory;
        private readonly string serviceUrl = "/travel";

        public TravelService(IHttpClientFactory _clientFactory, IConfiguration configuration) : base(_clientFactory)
        {
            clientFactory = _clientFactory;
        }

        public async Task<T> GetCurrentPage<T>(TravelDTO obj)
        {
            return await SendAsync<T>(new APIRequest()
            {
                ApiType = ApiType.POST,
                Data = obj,
                Url = baseUrl + serviceUrl + "/get"
            });
        }

        public async Task<T> PushNewPage<T>(TravelDTO obj)
        {
            return await SendAsync<T>(new APIRequest()
            {
                ApiType = ApiType.POST,
                Data = obj,
                Url = baseUrl + serviceUrl + "/go"
            });
        }

        public async Task<T> BreakChar<T>(string name)
        {
            return await SendAsync<T>(new APIRequest()
            {
                ApiType = ApiType.POST,
                Data = name,
                Url = baseUrl + serviceUrl + "/break"
            });
        }
    }
}

