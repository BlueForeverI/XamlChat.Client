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

        private string sessionKey;

        private ObservableCollection<MissedConversationModel> conversations;

        private ObservableCollection<UserModel> contacts;

        private UserModel currentUser;
        private ICommand closeConversation;
        private ICommand viewProfile;
        private ICommand startConversation;
        private ICommand goToSearhContacts;
        private ICommand goToProfile;

        public IEnumerable<MissedConversationModel> Conversations
        {
            get
            {
                if (this.conversations == null)
                {
                    this.Conversations = ConversationsPersister.GetMissed(sessionKey, this.currentUser.Id);
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

        public IEnumerable<UserModel> Contacts
        {
            get
            {
                if (this.contacts == null)
                {
                    this.Contacts = ContactsPersister.GetAllContacts(sessionKey);
                }
                return this.contacts;
            }
            set
            {
                if (this.contacts == null)
                {
                    this.contacts = new ObservableCollection<UserModel>();
                }
                this.contacts.Clear();
                foreach (var item in value)
                {
                    this.contacts.Add(item);
                }
            }
        }

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
            var newConversation = ConversationsPersister.Start(sessionKey, new ConversationModel()
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
            //this.conversations = new ObservableCollection<MissedConversationModel>()
            //{
            //    new MissedConversationModel()
            //    {
            //        Username="Stamat",
            //    },
            //    new MissedConversationModel()
            //    {
            //        Username="Mira"
            //    }
            //};
            //this.contacts = new ObservableCollection<UserModel>()
            //{
            //    new UserModel()
            //    {
            //        Username="Contact 1",
            //        ProfilePictureUrl="http://images2.fanpop.com/image/photos/9600000/Batman-Logo-batman-9683803-1280-1024.jpg"
            //    },
            //    new UserModel()
            //    {
            //        Username="Contact 2"
            //    }
            //};
        }
    }
}