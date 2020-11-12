using System.Net.Http;
using System.Threading.Tasks;
using JhipsterXamarin.Models;

namespace JhipsterXamarin.Services
{
    public interface IAuthenticationService
    {
        bool IsAuthenticated { get; set; }
        UserModel CurrentUser { get; set; }
        Task<bool> SignIn(LoginModel model);
        void SignOut();
        Task SetUserAndAuthorizationHeader(JwtToken token, bool save = false);
    }
}