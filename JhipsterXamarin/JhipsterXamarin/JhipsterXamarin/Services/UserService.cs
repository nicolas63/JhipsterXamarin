using JhipsterXamarin.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace JhipsterXamarin.Services
{
    public class UserService : MyEntityService<UserModel>, IUserService
    {
        private HttpClient _httpClient;

        public UserService(HttpClient httpClient, IAuthenticationService authenticationService) : base(httpClient, authenticationService)
        {
        }

        public async Task<IEnumerable<string>> GetAllAuthorities()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<string>>("/api/users/authorities");
        }

        public Task Add(UserModel model)
        {
            throw new System.NotImplementedException();
        }

        public Task Delete(string id)
        {
            throw new System.NotImplementedException();
        }

        public Task<UserModel> Get(string id)
        {
            throw new System.NotImplementedException();
        }

        public Task<IList<UserModel>> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public Task Update(UserModel model)
        {
            throw new System.NotImplementedException();
        }
    }
}
