using Client.MVVM.Model;
using Client.MVVM.Model.DTO;
using Client.MVVM.Model.DTO.Auth;
using Client.MVVM.Model.Utilities;
using Client.Services.IServices;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Services
{
    public class AuthService : BaseService, IAuthService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly string serviceUrl = "/auth";

        public AuthService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public Task<T> LoginAsync<T>(LoginRequestDTO obj)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = ApiType.POST,
                Data = obj,
                Url = BaseUrl.Get() + serviceUrl + "/login"
            });
        }

        public Task<T> RegisterAsync<T>(RegRequestDTO obj)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = ApiType.POST,
                Data = obj,
                Url = BaseUrl.Get() + serviceUrl + "/reg"
            });
        }
    }
}
