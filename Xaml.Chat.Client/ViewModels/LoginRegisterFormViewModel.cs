using System;
using System.Text;
using System.Windows.Controls;
using System.Windows.Input;
using Xaml.Chat.Client.Behavior;
using Xaml.Chat.Client.Helpers;

namespace Xaml.Chat.Client.ViewModels
{
    public class LoginRegisterFormViewModel : ViewModelBase, IPageViewModel
    {
        private string message;
        private ICommand loginCommand;
        private ICommand registerCommand;

        public string Name
        {
            get
            {
                return "Login Form";
            }
        }

        public string Username { get; set; }

        public string Email { get; set; }

        public string Message
        {
            get
            {
                return this.message;
            }
            set
            {
                this.message = value;
                this.OnPropertyChanged("Message");
            }
        }

        public ICommand Login
        {
            get
            {
                if (this.loginCommand == null)
                {
                    this.loginCommand = new RelayCommand(this.HandleLoginCommand);
                }
                return this.loginCommand;
            }
        }

        public ICommand Register
        {
            get
            {
                if (this.registerCommand == null)
                {
                    this.registerCommand = new RelayCommand(this.HandleRegisterCommand);
                }
                return this.registerCommand;
            }
        }

        public event EventHandler<LoginSuccessArgs> LoginSuccess;

        public LoginRegisterFormViewModel()
        {
            this.Username = "DonchoMinkov";
            this.Email = "Minkov@doncho.com";
        }
        
        private void HandleRegisterCommand(object parameter)
        {
            var passwordBox = parameter as PasswordBox;
            var password = passwordBox.Password;

          
            var authenticationCode = Sha1Encrypter.CalculateSHA1(password);

            // TODO: our own registration logic
            //DataPersister.RegisterUser(this.Username, this.Email, authenticationCode);
            this.HandleLoginCommand(parameter);
        }

        private void HandleLoginCommand(object parameter)
        {
            var passwordBox = parameter as PasswordBox;
            var password = passwordBox.Password;
            var authenticationCode = Sha1Encrypter.CalculateSHA1(password);

            // TODO: our own login logic
            //var username = DataPersister.LoginUser(this.Username, authenticationCode);

            //if (!string.IsNullOrEmpty(username))
            //{
            //    this.RaiseLoginSuccess(username);
            //}
        }

    


        protected void RaiseLoginSuccess(string username)
        {
            if (this.LoginSuccess!= null)
            {
                this.LoginSuccess(this, new LoginSuccessArgs(username));                
            }
        }
    }
}