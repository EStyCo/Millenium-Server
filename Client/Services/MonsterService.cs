using Client.MVVM.Model;
using Client.MVVM.Model.DTO;
using Client.MVVM.Model.Utilities;
using Microsoft.Extensions.Configuration;

namespace Client.Services
{
    public class MonsterService : BaseService
    {
        private readonly IHttpClientFactory clientFactory;
        private readonly string serviceUrl = "/monster";

        public MonsterService(IHttpClientFactory _clientFactory, IConfiguration configuration) : base(_clientFactory)
        {
            clientFactory = _clientFactory;
        }

        public async Task<T> AddMonster<T>(PlaceDTO dto)
        {
            return await SendAsync<T>(new APIRequest()
            {
                ApiType = ApiType.POST,
                Data = dto,
                Url = baseUrl + serviceUrl + "/add"
            });
        }

        public async Task<T> DeleteMonster<T>(DeleteMonsterDTO dto)
        {
            return await SendAsync<T>(new APIRequest()
            {
                ApiType = ApiType.POST,
                Data = dto,
                Url = baseUrl + serviceUrl + "/delete"
            });
        }

        public async Task<T> AttackMonster<T>(AttackMonsterDTO dto)
        {
            return await SendAsync<T>(new APIRequest()
            {
                ApiType = ApiType.POST,
                Data = dto,
                Url = baseUrl + serviceUrl + "/attack"
            });
        }
    }
}
