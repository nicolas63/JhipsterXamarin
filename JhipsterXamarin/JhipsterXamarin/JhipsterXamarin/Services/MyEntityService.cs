using JhipsterXamarin.Models;
using MvvmCross.Base;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace JhipsterXamarin.Services
{
    public class MyEntityService : IMyEntityService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUri = "http://10.0.2.2:8080";
        private const string ListEntitiesUrl = "api/myentities";

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
            await _httpClient.PostAsJsonAsync<MyEntityModelSimple>($"{BaseUri}/{ListEntitiesUrl}", entity);
        }

        public async Task DeleteEntity(MyEntityModel currentElement)
        {
            await _httpClient.DeleteAsync($"{BaseUri}/{ListEntitiesUrl}/{currentElement.Id}");
        }

        public async Task UpdateEntity(MyEntityModel currentElement)
        {
            await _httpClient.PutAsJsonAsync<MyEntityModel>($"{BaseUri}/{ListEntitiesUrl}", currentElement);
        }
    }
}
