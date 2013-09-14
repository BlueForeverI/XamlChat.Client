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

        public ConversationViewModel ConversationVM { get; set; }
        public RegisterFormViewModel RegisterFormVM { get; set; }
        public GeneralViewModel GeneralVM { get; set; }
        public LoginViewModel LoginVM { get; set; }

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
            this.ProfileVM = new ProfileViewModel(CurrentUserSetting); 
            this.ConversationVM = new ConversationViewModel();
            var registerVM = new RegisterFormViewModel();
            registerVM.RegisterSuccess += this.RegisterSuccessfull;
            this.RegisterFormVM = registerVM;

            this.GeneralVM = new GeneralViewModel();

            var loginVM = new LoginViewModel();
            loginVM.LoginSuccess += this.LoginSuccessful;
            this.LoginVM = loginVM;

            this.CurrentViewModel = this.LoginVM;
        }

        private void RegisterSuccessfull(object sender, RegisterSuccessArgs e)
        {
            this.CurrentUserSetting = e.RegisteredUser;
            this.CurrentViewModel = this.GeneralVM;
        }

        public void LoginSuccessful(object sender, LoginSuccessArgs e)
        {
            this.CurrentUserSetting = e.UserSetting;
            this.CurrentViewModel = this.GeneralVM;
        }

        public string Username { get; set; }
    }
}