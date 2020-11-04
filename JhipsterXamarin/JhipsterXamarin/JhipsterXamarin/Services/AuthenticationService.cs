using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using JhipsterXamarin.Models;

namespace JhipsterXamarin.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private const string BaseUri = "http://10.0.2.2:8080";
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
        }

        public async Task<bool> SignIn(LoginModel loginModel)
        {
            var result = await _httpClient.PostAsJsonAsync($"{BaseUri}/{AuthenticationUrl}", loginModel);
            if (result.IsSuccessStatusCode)
            {
                JwtToken = await result.Content.ReadFromJsonAsync<JwtToken>();
                await SetUserAndAuthorizationHeader(JwtToken);
            }

            return IsAuthenticated;
        }

        public void SignOut()
        {
            _httpClient.DefaultRequestHeaders.Remove(AuthorizationHeader);
            JwtToken = null;
            IsAuthenticated = false;
            CurrentUser = null;
        }

        private async Task SetUserAndAuthorizationHeader(JwtToken jwtToken)
        {
            IsAuthenticated = true;
            _httpClient.DefaultRequestHeaders.Remove(AuthorizationHeader);
            _httpClient.DefaultRequestHeaders.Add(AuthorizationHeader, $"Bearer {jwtToken.IdToken}");
            try
            {
                CurrentUser = await _httpClient.GetFromJsonAsync<UserModel>($"{BaseUri}/{AccountUrl}");
            }
            catch
            {
                IsAuthenticated = false;
            }
        }
    }
}