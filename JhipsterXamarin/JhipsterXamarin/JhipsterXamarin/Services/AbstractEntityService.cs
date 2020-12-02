using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using JhipsterXamarin.Models;

namespace JhipsterXamarin.Services
{
    public class AbstractEntityService<T> : IAbstractEntityService<T> where T : class
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public AbstractEntityService(HttpClient httpClient, string baseUrl)
        {
            _httpClient = httpClient;
            _baseUrl = baseUrl;
        }

        public async Task<List<T>> GetEntities()
        {
            return await _httpClient.GetFromJsonAsync<List<T>>(_baseUrl);
        }

        public async Task<T> GetEntity(long id)
        {
            return await _httpClient.GetFromJsonAsync<T>($"{_baseUrl}/{id}");
        }

        public async Task CreateEntity(T entity)
        {
            await _httpClient.PostAsJsonAsync(_baseUrl, entity);
        }

        public async Task DeleteEntity(long id)
        {
            await _httpClient.DeleteAsync($"{_baseUrl}/{id}");
        }

        public async Task UpdateEntity(T entity)
        {
            await _httpClient.PutAsJsonAsync(_baseUrl, entity);
        }
    }
}