using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using JhipsterXamarin.Models;

namespace JhipsterXamarin.Services
{
    public interface IAuthenticationService
    {
        bool IsAuthenticated { get; set; }
        UserModel CurrentUser { get; set; }
        HttpClient _httpClient { get; set; }
        Task<bool> SignIn(LoginModel model);
        string SignOut();
    }
}