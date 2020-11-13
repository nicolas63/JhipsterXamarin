using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using JhipsterXamarin.Models;

namespace JhipsterXamarin.Services
{
    public class RegisterService : IRegisterService
    {
        private const string RegisterUrl = "/api/register";
        private readonly HttpClient _httpClient;

        public RegisterService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(Configuration.BaseUri);
        }

        public async Task<HttpResponseMessage> Save(UserSaveModel registerModel)
        {
            return await _httpClient.PostAsJsonAsync(RegisterUrl, registerModel);
        }
    }
}
