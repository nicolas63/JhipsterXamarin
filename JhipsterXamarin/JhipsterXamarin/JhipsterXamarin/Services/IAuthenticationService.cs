using JhipsterXamarin.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace JhipsterXamarin.Services
{
    public interface IAuthenticationService
    {
        Task<bool> SignIn(LoginModel model);
        void SignOut();
    }
}
