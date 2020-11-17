﻿using JhipsterXamarin.Models;
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

        public virtual async Task<IList<T>> GetAll()
        {
            return await _httpClient.GetFromJsonAsync<IList<T>>(BaseUrl);
        }

        public virtual async Task<T> Get(string id)
        {
            return await _httpClient.GetFromJsonAsync<T>($"{BaseUrl}/{id}");
        }

        public virtual async Task Add(T model)
        {
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
