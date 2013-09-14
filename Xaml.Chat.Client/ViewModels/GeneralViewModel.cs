namespace Xaml.Chat.Client.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
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

        public IEnumerable<MissedConversationModel> Conversations
        {
            get
            {
                if (this.conversations==null)
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
                if (this.contacts==null)
                {
                    this.Contacts = ContactsPersister.GetAllContacts(sessionKey);
                }
                return this.contacts;
            }
            set
            {
                if (this.contacts==null)
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

        public GeneralViewModel()
        {
            this.conversations = new ObservableCollection<MissedConversationModel>()
            {
                new MissedConversationModel()
                {
                    Username="Stamat"
                },
                new MissedConversationModel()
                {
                    Username="Mira"
                }
            };
            this.contacts = new ObservableCollection<UserModel>()
            {
                new UserModel()
                {
                    Username="Contact 1"
                },
                new UserModel()
                {
                    Username="Contact 2"
                }
            };
        }    
    }
}