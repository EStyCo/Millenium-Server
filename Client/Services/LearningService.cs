using Client.MVVM.Model;
using Client.MVVM.Model.DTO;
using Client.MVVM.Model.Utilities;
using Microsoft.Extensions.Configuration;

namespace Client.Services
{
    public class LearningService : BaseService
    {
        private readonly IHttpClientFactory clientFactory;
        private readonly string serviceUrl = "/learning";

        public LearningService(IHttpClientFactory _clientFactory, IConfiguration configuration) : base(_clientFactory)
        {
            clientFactory = _clientFactory;
        }

        public async Task<T> GetCharacterSkills<T>(NameRequestDTO name)
        {
            return await SendAsync<T>(new APIRequest()
            {
                ApiType = ApiType.POST,
                Data = name,
                Url = baseUrl + serviceUrl + "/get"
            });
        }

        public async Task<T> LearnSkill<T>(LearnSkillDTO dto)
        {
            return await SendAsync<T>(new APIRequest()
            {
                ApiType = ApiType.POST,
                Data = dto,
                Url = baseUrl + serviceUrl + "/learn"
            });
        }

        public async Task<T> ForgotSkill<T>(LearnSkillDTO dto)
        {
            return await SendAsync<T>(new APIRequest()
            {
                ApiType = ApiType.POST,
                Data = dto,
                Url = baseUrl + serviceUrl + "/forgot"
            });
        }
    }
}
