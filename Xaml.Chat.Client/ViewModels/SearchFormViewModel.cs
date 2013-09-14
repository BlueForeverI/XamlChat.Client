using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Xaml.Chat.Client.Behavior;
using Xaml.Chat.Client.Data;
using Xaml.Chat.Client.Models;

namespace Xaml.Chat.Client.ViewModels
{
    public class SearchFormViewModel : ViewModelBase, IPageViewModel
    {
        public SearchFormViewModel()
        {
        }

        public SearchFormViewModel(string sessionKey)
        {
            this.SessionKey = sessionKey;
        }

        public string SessionKey;
        public string Name { get { return "Search Form"; } }

        public string QueryText { get; set; }
        public IEnumerable<UserModel> FoundUsers { get; set; } 

        private ICommand search;
        public ICommand Search
        {
            get
            {
                if(this.search == null)
                {
                    this.search = new RelayCommand(HandleSearch);
                }

                return this.search;
            }
        }

        private ICommand addToContacts;
        public ICommand AddToContacts
        {
            get
            {
                if(this.addToContacts == null)
                {
                    this.addToContacts = new RelayCommand(HandleAddToContacts);
                }

                return this.addToContacts;
            }
        }

        private void HandleAddToContacts(object parameter)
        {
            int index = (int) parameter;
            var userToAdd = FoundUsers.ElementAt(index);

            try
            {
                ContactsPersister.AddUserToContacts(this.SessionKey, userToAdd.Id);
                MessageBox.Show("Contact request sent!");
            }
            catch (Exception)
            {
                MessageBox.Show("Error");
            }
        }

        private void HandleSearch(object parameter)
        {
            try
            {
                var foundUsers = UserPersister.Search(this.SessionKey, new QueryModel() {QueryText = this.QueryText});
                this.FoundUsers = foundUsers;
                OnPropertyChanged("FoundUsers");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error");
            }
        }
    }
}
