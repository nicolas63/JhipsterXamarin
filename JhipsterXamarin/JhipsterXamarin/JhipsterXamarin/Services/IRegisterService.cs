using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using JhipsterXamarin.Models;

namespace JhipsterXamarin.Services
{
    public interface IRegisterService
    {
        Task<HttpResponseMessage> Save(UserSaveModel registerModel);
    }
}
