using JhipsterXamarin.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace JhipsterXamarin.Services
{
    public partial class UserUpdate
    {
        public string Id { get; set; }

        private IUserService UserService { get; set; }

        private UserModel CurrentUser { get; set; }

        private IEnumerable<string> Authorities { get; set; } = new List<string>();

        private IEnumerable<string> SelectedAuthorities { get; set; } = new List<string>();
        private IReadOnlyList<string> CurrentAuthorities { get; set; } = new List<string>();

        protected async Task OnInitializedAsync()
        {
            CurrentUser = null;
            if (!string.IsNullOrWhiteSpace(Id))
            {
                CurrentUser = await UserService.Get(Id);
                SelectedAuthorities = CurrentUser.Authorities;
                CurrentAuthorities = new ReadOnlyCollection<string>(CurrentUser.Authorities.ToList());
            }
            else
            {
                CurrentUser = new UserModel();
            }

            Authorities = await UserService.GetAllAuthorities();
        }

        private void OnSelectAuthoritiesChanged(IReadOnlyList<string> selectedAuthorities)
        {
            SelectedAuthorities = selectedAuthorities;
        }

        private async Task Save()
        {
            CurrentUser.Authorities = SelectedAuthorities;
            if (!string.IsNullOrWhiteSpace(Id))
            {
                await UserService.Update(CurrentUser);
            }
            else
            {
                await UserService.Add(CurrentUser);
            }
        }
    }
}
