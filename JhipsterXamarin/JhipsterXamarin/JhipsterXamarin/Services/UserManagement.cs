using JhipsterXamarin.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JhipsterXamarin.Services
{
    public  class UserManagement
    {
        private IList<UserModel> UserModels { get; set; }
        private IUserService UserService { get; set; }

        private IAuthenticationService AuthenticationService;

        protected  async Task OnInitializedAsync()
        {
            UserModels = await UserService.GetAll();
        }

        private async Task ActiveUser(UserModel user, bool activated)
        {
            user.Activated = activated;
            await UserService.Update(user);
        }

        private async Task DeleteUser(string login)
        {
            foreach (UserModel userModel in UserModels)
            {
                if (userModel.Login == login)
                {
                    await UserService.Delete(login);
                    UserModels.Remove(UserModels.First(user => user.Login.Equals(login)));
                }
            }
        }
    }
}
