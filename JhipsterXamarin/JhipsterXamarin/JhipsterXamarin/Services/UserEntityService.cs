using JhipsterXamarin.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace JhipsterXamarin.Services
{
    public class UserEntityService<T> where T : class
    {
        private const string AuthorizationHeader = "Authorization";

        protected readonly HttpClient _httpClient;
        private IAuthenticationService authenticationService;

        protected JwtToken JwtToken { get; set; }
        protected string BaseUrl { get; }

        public UserEntityService(HttpClient httpClient, IAuthenticationService authenticationService, string baseUrl)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(Configuration.BaseUri);
            JwtToken = authenticationService.JwtToken;
            if (JwtToken != null)
            {
                _httpClient.DefaultRequestHeaders.Add(AuthorizationHeader, $"Bearer {JwtToken.IdToken}");
            }
            BaseUrl = baseUrl;
        }

        public UserEntityService(HttpClient httpClient, IAuthenticationService authenticationService)
        {
            this._httpClient = httpClient;
            this.authenticationService = authenticationService;
        }

        public virtual async Task<IList<T>> GetAll()
        {
            return await _httpClient.GetFromJsonAsync<IList<T>>(BaseUrl);
        }

        public virtual async Task<T> Get(string id)
        {
            return await _httpClient.GetFromJsonAsync<T>($"{BaseUrl}/{id}");
        }

        public virtual async Task Add(string login, string firstName, string lastName,string current)
        {
            var model = new UserModel();
            model.Login = login;
            model.FirstName = firstName;
            model.LastName = lastName;
            model.LastModifiedDate = DateTime.Now;
            model.LastModifiedBy = current;
            await _httpClient.PostAsJsonAsync(BaseUrl, model);
        }
        public virtual async Task Update(T model)
        {
            await _httpClient.PutAsJsonAsync(BaseUrl, model);
        }

        public virtual async Task Delete(string id)
        {
            await _httpClient.DeleteAsync($"{BaseUrl}/{id}");
        }

    }
}
