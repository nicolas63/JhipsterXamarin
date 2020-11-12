﻿using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Akavache;
using JhipsterXamarin.Models;

namespace JhipsterXamarin.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private const string AuthenticationUrl = "api/authenticate";
        private const string AccountUrl = "api/account";
        private const string AuthorizationHeader = "Authorization";
        private readonly HttpClient _httpClient;

        public bool IsAuthenticated { get; set; }
        public UserModel CurrentUser { get; set; }
        private JwtToken JwtToken { get; set; }

        public AuthenticationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(Configuration.BaseUri);
        }

        public async Task<bool> SignIn(LoginModel loginModel)
        {
            var result = await _httpClient.PostAsJsonAsync(AuthenticationUrl, loginModel);
            if (result.IsSuccessStatusCode)
            {
                JwtToken = await result.Content.ReadFromJsonAsync<JwtToken>();
                await SetUserAndAuthorizationHeader(JwtToken, loginModel.RememberMe);
            }
            return IsAuthenticated;
        }

        public async Task<bool> SignIn(JwtToken jwtToken)
        {
            await SetUserAndAuthorizationHeader(jwtToken);
            return IsAuthenticated;
        }

        public void SignOut()
        {
            _httpClient.DefaultRequestHeaders.Remove(AuthorizationHeader);
            JwtToken = null;
            IsAuthenticated = false;
            CurrentUser = null;
            BlobCache.Secure.InvalidateAll();
        }

        public async Task SetUserAndAuthorizationHeader(JwtToken jwtToken, bool save = false)
        {
            IsAuthenticated = true;
            _httpClient.DefaultRequestHeaders.Remove(AuthorizationHeader);
            _httpClient.DefaultRequestHeaders.Add(AuthorizationHeader, $"Bearer {jwtToken.IdToken}");
            try
            {
                CurrentUser = await _httpClient.GetFromJsonAsync<UserModel>(AccountUrl);
                if (save)
                {
                    await BlobCache.Secure.InvalidateAll();
                    await BlobCache.Secure.InsertObject<JwtToken>("token", jwtToken);
                }
            }
            catch
            {
                IsAuthenticated = false;
            }
        }
    }
}