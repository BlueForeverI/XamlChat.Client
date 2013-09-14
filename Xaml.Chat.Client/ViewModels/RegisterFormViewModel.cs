using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Xaml.Chat.Client.Behavior;
using Xaml.Chat.Client.Data;
using Xaml.Chat.Client.Helpers;
using Xaml.Chat.Client.Models;

namespace Xaml.Chat.Client.ViewModels
{
    public class RegisterFormViewModel : ViewModelBase, IPageViewModel
    {
        public string Name { get { return "Register Form"; } }

        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        private ICommand register;
        public ICommand Register
        {
            get
            {
                if(this.register == null)
                {
                    this.register = new RelayCommand(HandleRegister);
                }

                return this.register;
            }
        }

        public event EventHandler<RegisterSuccessArgs> RegisterSuccess;
 
        private void RaiseRegisterSuccess(UserModel registeredUser)
        {
            if(this.RegisterSuccess != null)
            {
                this.RegisterSuccess(this, new RegisterSuccessArgs(registeredUser));
            }
        }

        private void HandleRegister(object parameter)
        {
            var passwordBox = parameter as PasswordBox;
            string password = passwordBox.Password;
            string passwordHash = Sha1Encrypter.CalculateSHA1(password);

            try
            {

                var registeredUser = UserPersister.RegisterUser(new RegisterUserModel()
                {
                    Username = this.Username,
                    PasswordHash = passwordHash,
                    FirstName = this.FirstName,
                    LastName = this.LastName,
                    ProfilePictureUrl =
                        "http://images1.wikia.nocookie.net/__cb20120325053014/mugen/images/6/6f/Spiderman.png"
                });

                RaiseRegisterSuccess(registeredUser);
            }
            catch (Exception)
            {
                MessageBox.Show("NOT registered");
            }
        }
    }
}
