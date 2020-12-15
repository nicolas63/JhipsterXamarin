using System.Threading.Tasks;
using JhipsterXamarin.Models;

namespace JhipsterXamarin.Services
{
    public interface IAuthenticationService
    {
        bool IsAuthenticated { get; set; }
        UserModel CurrentUser { get; set; }
        JwtToken JwtToken { get; }
        Task<bool> SignIn(LoginModel model);
        void SignOut();
    }
}