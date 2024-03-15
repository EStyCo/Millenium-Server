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

        public async Task<T> GetMonsters<T>()
        {
            return await SendAsync<T>(new APIRequest()
            {
                ApiType = ApiType.GET,
                Url = baseUrl + serviceUrl + "/get"
            });
        }

        public async Task<T> AddMonster<T>()
        {
            return await SendAsync<T>(new APIRequest()
            {
                ApiType = ApiType.GET,
                Url = baseUrl + serviceUrl + "/add"
            });
        }

        public async Task<T> DeleteMonster<T>(int id)
        {
            return await SendAsync<T>(new APIRequest()
            {
                ApiType = ApiType.POST,
                Data = id,
                Url = baseUrl + serviceUrl + "/delete"
            });
        }

        public async Task<T> AttackMonster<T>(AttackMonsterDTO attackMonster)
        {
            return await SendAsync<T>(new APIRequest()
            {
                ApiType = ApiType.POST,
                Data = attackMonster,
                Url = baseUrl + serviceUrl + "/attack"
            });
        }
    }
}
