using JhipsterXamarin.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JhipsterXamarin.Services
{
    public interface IUserEntityService<T> where T : UserModel
    {
        Task<IList<UserModel>> GetAll();
        Task<UserModel> Get(string id);
        Task Add(UserModel _model);
        Task Update(UserModel model);
        Task Delete(string id);
    }
}
