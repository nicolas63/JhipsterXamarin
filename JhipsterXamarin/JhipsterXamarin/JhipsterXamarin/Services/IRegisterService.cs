using System.Threading.Tasks;
using JhipsterXamarin.Models;

namespace JhipsterXamarin.Services
{
    public interface IRegisterService
    {
        Task<string> Save(UserSaveModel registerModel);
    }
}
