using JhipsterXamarin.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace JhipsterXamarin.Services
{
    public class UserEntityService : IUserEntityService<UserModel>
    {

        protected readonly HttpClient _httpClient;

        protected string BaseUrl = "api/users";
        public UserEntityService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }


        public virtual async Task<IList<UserModel>> GetAll()
        { 
            return await _httpClient.GetFromJsonAsync<IList<UserModel>>(BaseUrl);
        }

        public virtual async Task<UserModel> Get(string id)
        {
            return await _httpClient.GetFromJsonAsync<UserModel>($"{BaseUrl}/{id}");
        }

        public virtual async Task Add(UserModel model)
        {
            await _httpClient.PostAsJsonAsync(BaseUrl, model);
        }
        public virtual async Task Update(UserModel model)
        {
            await _httpClient.PutAsJsonAsync(BaseUrl, model);
        }

        public virtual async Task Delete(string login)
        {
            await _httpClient.DeleteAsync($"{BaseUrl}/{login}");
        }

    }
}
