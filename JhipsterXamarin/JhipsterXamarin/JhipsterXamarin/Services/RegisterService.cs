using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using JhipsterXamarin.Models;
using MvvmCross.Logging;
using SharedModel.Constants;

namespace JhipsterXamarin.Services
{
    public class RegisterService : IRegisterService
    {
        private const string RegisterUrl = "/api/register";
        private readonly HttpClient _httpClient;
        private readonly IMvxLog _log;

        public RegisterService(HttpClient httpClient, IMvxLog log)
        {
            _httpClient = httpClient;
            _log = log;
        }

        public async Task<string> Save(UserSaveModel registerModel)
        {
            var resp = await _httpClient.PostAsJsonAsync(RegisterUrl, registerModel);
            if (resp.IsSuccessStatusCode)
                return null;
            return await ProcessError(resp);
        }

        private async Task<string> ProcessError(HttpResponseMessage result)
        {
            if (result.StatusCode != HttpStatusCode.BadRequest) return ErrorConst.UnknownErrorType;

            try
            {
                var res = await result.Content.ReadFromJsonAsync<RegisterResultRequest>();
                return res.Type;
            }
            catch (Exception ex)
            {
                _log.ErrorException("Failed to parse JSON from error", ex);
                return ErrorConst.UnknownErrorType;
            }
        }
    }
}
