using JhipsterXamarin.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace JhipsterXamarin.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private const string BaseUri = "http://10.0.2.2:5001/";
        private const string AuthenticatationUrl = "api/authenticate";
        private const string AccountUrl = "api/account";
        private const string AuthorizationHeader = "Authorization";

        public bool IsAuthenticated { get; set; }
        public JwtToken JwtToken { get; set; }
        public UserModel CurrentUser { get; set; }

        readonly HttpClient _httpClient;

        public AuthenticationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> SignIn(LoginModel loginModel)
        {
            var result = await _httpClient.PostAsJsonAsync(BaseUri + AuthenticatationUrl, loginModel);
            if (result.IsSuccessStatusCode)
            {
                JwtToken = await result.Content.ReadFromJsonAsync<JwtToken>();
                await SetUserAndAuthorizationHeader(JwtToken);
            }
            return IsAuthenticated;
        }

        private async Task SetUserAndAuthorizationHeader(JwtToken jwtToken)
        {
            IsAuthenticated = true;
            _httpClient.DefaultRequestHeaders.Remove(AuthorizationHeader);
            _httpClient.DefaultRequestHeaders.Add(AuthorizationHeader, $"Bearer {jwtToken.IdToken}");
            try
            {
                CurrentUser = await _httpClient.GetFromJsonAsync<UserModel>(BaseUri + AccountUrl);
            }
            catch
            {
                IsAuthenticated = false;
            }
        }

        public void SignOut()
        {
            _httpClient.DefaultRequestHeaders.Remove(AuthorizationHeader);
            JwtToken = null;
            IsAuthenticated = false;
            CurrentUser = null;
        }
    }
}
