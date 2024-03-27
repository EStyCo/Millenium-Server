using Client.MVVM.Model;
using Client.MVVM.Model.DTO;
using Client.MVVM.Model.Utilities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Client.Services
{
    public class TestService : BaseService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly string serviceUrl = "/test";

        public TestService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public Task<T> Test<T>()
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = ApiType.GET,
                Url = baseUrl + serviceUrl
            });
        }
    }
}
