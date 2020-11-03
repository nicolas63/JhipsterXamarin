using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using JhipsterXamarin.Models;

namespace JhipsterXamarin.Services
{
    public class MyEntityService : IMyEntityService
    {
        private const string BaseUri = "http://10.0.2.2:8080";
        private const string ListEntitiesUrl = "api/myentities";
        private readonly HttpClient _httpClient;

        public MyEntityService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<MyEntityModel>> GetEntities()
        {
            return await _httpClient.GetFromJsonAsync<List<MyEntityModel>>($"{BaseUri}/{ListEntitiesUrl}");
        }

        public async Task<MyEntityModel> GetEntity(int id)
        {
            return await _httpClient.GetFromJsonAsync<MyEntityModel>($"{BaseUri}/{ListEntitiesUrl}/{id}");
        }

        public async Task CreateEntity(string name, int age)
        {
            var entity = new MyEntityModelSimple();
            entity.Name = name;
            entity.Age = age;
            await _httpClient.PostAsJsonAsync($"{BaseUri}/{ListEntitiesUrl}", entity);
        }

        public async Task DeleteEntity(MyEntityModel currentElement)
        {
            await _httpClient.DeleteAsync($"{BaseUri}/{ListEntitiesUrl}/{currentElement.Id}");
        }

        public async Task UpdateEntity(MyEntityModel currentElement)
        {
            await _httpClient.PutAsJsonAsync($"{BaseUri}/{ListEntitiesUrl}", currentElement);
        }
    }
}