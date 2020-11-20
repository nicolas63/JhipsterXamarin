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

        private UserModel CurrentUser { get; set; }
        protected  async Task OnInitializedAsync()
        {
            UserModels = await UserService.GetAll();
        }

        private async Task ActiveUser(UserModel user, bool activated)
        {
            user.Activated = activated;
            await UserService.Update(user);
            CurrentUser = user; 
        }

        private async Task AddUser(UserModel _user)
        {
            if (!UserModels.Contains(UserModels.First(user => user.Login.Equals(_user.Login))))
            {
                await UserService.Add(_user.Login, _user.FirstName, _user.LastName, CurrentUser.FirstName + " | " + CurrentUser.LastName);
                UserModels.Add(_user);
                UserModels = await UserService.GetAll();
            }
        }
        private async Task UpdateUser(UserModel user)
        {
            foreach (UserModel userModel in UserModels)
            {
                if (userModel == user)
                {
                    await UserService.Update(user);
                    UserModels.Remove(UserModels.First(user => user.Login.Equals(userModel.Login)));
                    UserModels.Add(user);
                    UserModels = await UserService.GetAll();
                }
            }
        }
        private async Task DeleteUser(string login)
        {
            foreach (UserModel userModel in UserModels)
            {
                if (userModel.Login == login)
                {
                    await UserService.Delete(login);
                    UserModels.Remove(UserModels.First(user => user.Login.Equals(login)));
                    UserModels = await UserService.GetAll();
                }
            }
        }
    }
}
