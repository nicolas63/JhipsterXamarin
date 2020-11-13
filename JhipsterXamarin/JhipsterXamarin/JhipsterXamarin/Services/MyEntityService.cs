using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using JhipsterXamarin.Models;

namespace JhipsterXamarin.Services
{
    public class MyEntityService<T> : IMyEntityService
    {
        private const string ListEntitiesUrl = "api/myentities";
        private readonly string ListUser = "/api/users";
        private readonly HttpClient _httpClient;

        public MyEntityService(HttpClient httpClient, IAuthenticationService authenticationService)
        {
            _httpClient = httpClient;
        }

        public async Task<List<MyEntityModel>> GetEntities()
        {
            return await _httpClient.GetFromJsonAsync<List<MyEntityModel>>(ListEntitiesUrl);
        }

        public async Task<MyEntityModel> GetEntity(int id)
        {
            return await _httpClient.GetFromJsonAsync<MyEntityModel>($"{ListEntitiesUrl}/{id}");
        }

        public async Task CreateEntity(string name, int age)
        {
            var entity = new MyEntityModelSimple();
            entity.Name = name;
            entity.Age = age;
            await _httpClient.PostAsJsonAsync(ListEntitiesUrl, entity);
        }

        public async Task DeleteEntity(MyEntityModel currentElement)
        {
            await _httpClient.DeleteAsync($"{ListEntitiesUrl}/{currentElement.Id}");
        }

        public async Task UpdateEntity(MyEntityModel currentElement)
        {
            await _httpClient.PutAsJsonAsync(ListEntitiesUrl, currentElement);
        }
    }
}