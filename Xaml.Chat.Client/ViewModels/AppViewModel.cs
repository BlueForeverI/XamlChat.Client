namespace Xaml.Chat.Client.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Input;
    using Xaml.Chat.Client.Behavior;
    using Xaml.Chat.Client.Helpers;
    using Xaml.Chat.Client.Models;

    public class AppViewModel : ViewModelBase
    {
        private ICommand changeViewModelCommand;

        public UserModel CurrentUserSetting { get; set; }

        private IPageViewModel currentViewModel;
        private bool loggedInUser = false;
        private ICommand logoutCommand;
        private ICommand goToSearhContacts;
        private ICommand goToProfile;
        private ICommand goToContactRequests;

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
        public SearchFormViewModel SearchVM { get; set; }
        public ContactRequestsViewModel ContactRequestsVM { get; set; }

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

        public ICommand GoToSearhContacts
        {
            get
            {
                if (this.goToSearhContacts == null)
                {
                    this.goToSearhContacts = new RelayCommand(this.HandleGoToSearch);
                }
                return this.goToSearhContacts;
            }
        }

        public ICommand GoToProfile
        {
            get
            {
                if (this.goToProfile == null)
                {
                    this.goToProfile = new RelayCommand(this.HanddleGoToProfile);
                }
                return this.goToProfile;
            }
        }

        public ICommand GoToHome
        {
            get
            {
                if (this.goToHome ==null)
                {
                    this.goToHome = new RelayCommand(this.HandleGoToHome);
                }
                return this.goToHome;
            }
        }

        public ICommand GoToContactRequests
        {
            get
            {
                if(this.goToContactRequests == null)
                {
                    this.goToContactRequests = new RelayCommand(HandleGoToContactRequests);
                }

                return this.goToContactRequests;
            }
        }

        private void HandleGoToContactRequests(object parameter)
        {
            this.ContactRequestsVM = new ContactRequestsViewModel(this.CurrentUserSetting.SessionKey);
            this.CurrentViewModel = this.ContactRequestsVM;
        }

        private void HandleGoToHome(object parameter)
        {
            this.CurrentViewModel = this.GeneralVM;

        }

        private void HanddleGoToProfile(object parameter)
        {
            this.ProfileVM = new ProfileViewModel(this.CurrentUserSetting);
            this.ProfileVM.EditSuccess += HandleProfileEditSuccess;
            this.CurrentViewModel = this.ProfileVM;
        }

        private void HandleProfileEditSuccess(object sender, LoginSuccessArgs e)
        {
            this.CurrentUserSetting = e.UserSetting;
        }

        private void HandleGoToSearch(object parameter)
        {
            this.CurrentViewModel = this.SearchVM;
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
            //this.ProfileVM = new ProfileViewModel(new UserModel()); 
            var registerVM = new RegisterFormViewModel();
            registerVM.RegisterSuccess += this.RegisterSuccessfull;
            this.RegisterFormVM = registerVM;

            var loginVM = new LoginViewModel();
            loginVM.LoginSuccess += this.LoginSuccessful;
            this.LoginVM = loginVM;

            this.SearchVM = new SearchFormViewModel();

            this.ContactRequestsVM = new ContactRequestsViewModel();

            this.CurrentViewModel = this.LoginVM;
        }

        private void InitializeGeneralViewModel()
        {
            var generavVM = new GeneralViewModel(CurrentUserSetting);
            generavVM.ConversationStarted += this.ConversationStartedHandler;
            this.GeneralVM = generavVM;
        }

        private void ConversationStartedHandler(object sender, ConversationStartedArgs e)
        {
            var conversation = e.Conversation;
            var partner = (conversation.FirstUser.Username == CurrentUserSetting.Username)
                                              ? conversation.SecondUser
                                              : conversation.FirstUser;


            this.ConversationVM = new ConversationViewModel(conversation, CurrentUserSetting, partner);
            this.CurrentViewModel = this.ConversationVM;
        }

        private void RegisterSuccessfull(object sender, RegisterSuccessArgs e)
        {
            this.CurrentUserSetting = e.RegisteredUser;
            InitializeGeneralViewModel();
            this.CurrentViewModel = this.GeneralVM;
            this.SearchVM.SessionKey = CurrentUserSetting.SessionKey;
        }

        public void LoginSuccessful(object sender, LoginSuccessArgs e)
        {
            this.CurrentUserSetting = e.UserSetting;
            InitializeGeneralViewModel();
            this.CurrentViewModel = this.GeneralVM;
            this.LoggedInUser = true;
            this.SearchVM.SessionKey = CurrentUserSetting.SessionKey;
        }

        public string Username { get; set; }

        public ICommand goToHome { get; set; }
    }
}