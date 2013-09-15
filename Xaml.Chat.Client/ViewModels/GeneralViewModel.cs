namespace Xaml.Chat.Client.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows.Data;
    using System.Windows.Input;
    using Xaml.Chat.Client.Behavior;
    using Xaml.Chat.Client.Data;
    using Xaml.Chat.Client.Models;

    public class GeneralViewModel : ViewModelBase, IPageViewModel
    {
        public string Name
        {
            get
            {
                return "General";
            }
        }

        public string SessionKey { get; set; }

        private ObservableCollection<MissedConversationModel> conversations;

        private ObservableCollection<UserModel> contacts;

        private UserModel currentUser;
        private ICommand closeConversation;
        private ICommand viewProfile;
        private ICommand startConversation;

        public IEnumerable<MissedConversationModel> Conversations
        {
            get
            {
                if (this.conversations == null)
                {
                    this.Conversations = ConversationsPersister.GetMissed(SessionKey, this.currentUser.Id);
                }
                return this.conversations;
            }
            set
            {
                if (this.conversations == null)
                {
                    this.conversations = new ObservableCollection<MissedConversationModel>();
                }
                this.conversations.Clear();
                foreach (var item in value)
                {
                    this.conversations.Add(item);
                }
            }
        }

        public IEnumerable<UserModel> Contacts { get; set; }

        public ICommand CloseConversation
        {
            get
            {
                if (this.closeConversation == null)
                {
                    this.closeConversation = new RelayCommand(this.HandleCloseConversation);
                }
                return this.closeConversation;
            }
        }

        public ICommand ViewProfile
        {
            get
            {
                if (this.viewProfile==null)
                {
                    this.viewProfile = new RelayCommand(this.HandleViewProfile);
                }
                return this.viewProfile;
            }
        }

        public ICommand StartConversation
        {
            get
            {
                if (this.startConversation==null)
                {
                    this.startConversation = new RelayCommand(this.HandleStartConversation);
                }
                return this.startConversation;
            }
        }

        

        private void HandleStartConversation(object parameter)
        {
            var view = CollectionViewSource.GetDefaultView(this.contacts);
            UserModel selectedUser = view.CurrentItem as UserModel;
            //TODO: Does it work with the services
            var newConversation = ConversationsPersister.Start(SessionKey, new ConversationModel()
            {
                SecondUser = selectedUser,
            });
            this.conversations.Add(new MissedConversationModel()
            {
                Username=newConversation.SecondUser.Username,
            });
        }

        private void HandleViewProfile(object parameter)
        {
            var view = CollectionViewSource.GetDefaultView(this.contacts);            
            UserModel selectedUser =view.CurrentItem as UserModel;
        }

        private void HandleCloseConversation(object parameter)
        {
            this.conversations.Remove(parameter as MissedConversationModel);
        }

        public GeneralViewModel()
        {
            this.Contacts = new List<UserModel>();
        }

        public GeneralViewModel(string sessionKey)
        {
            this.SessionKey = sessionKey;
            this.Contacts = ContactsPersister.GetAllContacts(this.SessionKey);
            OnPropertyChanged("Contacts");
        }
    }
}