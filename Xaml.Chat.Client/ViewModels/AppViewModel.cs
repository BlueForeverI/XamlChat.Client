using System.Collections.Generic;
using System.Windows.Input;
using Xaml.Chat.Client.Behavior;
using Xaml.Chat.Client.Helpers;
using Xaml.Chat.Client.Models;

namespace Xaml.Chat.Client.ViewModels
{
    public class AppViewModel : ViewModelBase
    {
        private ICommand changeViewModelCommand;

        public UserModel CurrentUserSetting { get; set; }

        private IPageViewModel currentViewModel;
        private bool loggedInUser = false;
        private ICommand logoutCommand;

        public IPageViewModel CurrentViewModel
        {
            get
            {
                return this.currentViewModel;
            }
            set
            {
                this.currentViewModel = value;
                this.OnPropertyChanged("CurrentViewModel");
            }
        }

        public bool LoggedInUser
        {
            get
            {
                return this.loggedInUser;
            }
            set
            {
                this.loggedInUser = value;
                this.OnPropertyChanged("LoggedInUser");
            }
        }

        public LoginRegisterFormViewModel LoginRegisterVM { get; set; }
        public ConversationViewModel ConversationVM { get; set; }

        public ProfileViewModel ProfileVM { get; set; }

        public List<IPageViewModel> ViewModels { get; set; }

        public ICommand ChangeViewModel
        {
            get
            {
                if (this.changeViewModelCommand == null)
                {
                    this.changeViewModelCommand =
                        new RelayCommand(this.HandleChangeViewModelCommand);
                }
                return this.changeViewModelCommand;
            }
        }

        public ICommand Logout
        {
            get
            {
                if (this.logoutCommand == null)
                {
                    this.logoutCommand = new RelayCommand(this.HandleLogoutCommand);
                }
                return this.logoutCommand;
            }
        }

        private void HandleLogoutCommand(object parameter)
        {
            // TODO: our own logout logic
            //bool isUserLoggedOut = DataPersister.LogoutUser();
            //if (isUserLoggedOut)
            //{
            //    this.Username = "";
            //    this.LoggedInUser = false;
            //    //this.CurrentViewModel = this.LoginRegisterVM;
            //    this.HandleChangeViewModelCommand(this.LoginRegisterVM);
            //}

        }

        private void HandleChangeViewModelCommand(object parameter)
        {
            var newCurrentViewModel = parameter as IPageViewModel;
            this.CurrentViewModel = newCurrentViewModel;
        }

        public AppViewModel()
        {
            // TODO: initialize our own views
            this.ViewModels = new List<IPageViewModel>();
            var loginVM = new LoginRegisterFormViewModel();
            loginVM.LoginSuccess += this.LoginSuccessful;
            this.ProfileVM = new ProfileViewModel(CurrentUserSetting); 


            this.LoginRegisterVM = loginVM;
            this.ConversationVM = new ConversationViewModel();
            this.CurrentViewModel = this.ConversationVM;
        }

        public void LoginSuccessful(object sender, LoginSuccessArgs e)
        {
            
        }

        public string Username { get; set; }
    }
}