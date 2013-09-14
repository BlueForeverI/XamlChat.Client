using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xaml.Chat.Client.Behavior;
using Xaml.Chat.Client.Models;

namespace Xaml.Chat.Client.ViewModels
{
    public class ProfileViewModel
    {
        public UserModel CurrentUserSettings { get; set; }

        public ProfileViewModel(UserModel currentUserSettings)
        {
            this.CurrentUserSettings = currentUserSettings;
        }

        public string Username { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string ProfilePictureUrl { get; set; }

        public string OldPassword { get; set; }

        //public UserModel NewUserSettings { get; set; }

        private ICommand editProfile;

        public ICommand EditProfile
        {
            get
            {
                if (this.editProfile == null)
                {
                    this.editProfile = new RelayCommand(this.HandleEditProfileCommand);  
                }
                return this.editProfile;
            }
        }

        private void HandleEditProfileCommand(object parameter)
        {
          var editProfile=new UserEditModel()
          {
            
          }

        }
    }
}
