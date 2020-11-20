using JhipsterXamarin.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JhipsterXamarin.Services
{
    public interface IUserService
    {
        public Task<IList<UserModel>> GetAll();

        public Task<UserModel> Get(string id);

        public Task Add(string login, string firstName, string lastName, string current);

        Task Update(UserModel model);

        Task Delete(string id);

        Task<IEnumerable<string>> GetAllAuthorities();
    }
}
