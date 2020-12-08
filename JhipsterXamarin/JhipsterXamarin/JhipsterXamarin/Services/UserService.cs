using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace JhipsterXamarin.Services
{
    public class UserService : UserEntityService, IUserService
    {
        public UserService(HttpClient httpClient, IAuthenticationService authenticationService) : base(httpClient, authenticationService)
        {
        }
        public async Task<IEnumerable<string>> GetAllAuthorities()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<string>>("/api/users/authorities");
        }

    }
}
