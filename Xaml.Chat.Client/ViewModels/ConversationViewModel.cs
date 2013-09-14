using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xaml.Chat.Client.Behavior;
using Xaml.Chat.Client.Helpers;
using Xaml.Chat.Client.Models;

namespace Xaml.Chat.Client.ViewModels
{
    public class ConversationViewModel : ViewModelBase, IPageViewModel
    {
        private const string PUBLISH_KEY = "pub-c-c91f17ec-a2d1-4afb-93d5-650cb2e2d610";
        private const string SUBSCRIBE_KEY = "sub-c-59c4b3f8-1d38-11e3-9231-02ee2ddab7fe";
        private const string SECRET_KEY = "sec-c-MzJhZjE1NmMtOWMxNC00NGViLWE1MDUtNGUyNzY3YWFkODE1";
        private PubnubAPI pubnub;
        private string channelName;

        public ConversationViewModel()
        {
            pubnub = new PubnubAPI(PUBLISH_KEY, SUBSCRIBE_KEY, SECRET_KEY, true);

            var minId = Math.Min(CurrentUserInfo.Id, Partner.Id);
            var maxId = Math.Max(CurrentUserInfo.Id, Partner.Id);
            channelName = string.Format("channel-{0}-{1}",
                                               minId, maxId);

            pubnub.Subscribe(channelName, HandleNewMessageReceived);
        }

        private bool HandleNewMessageReceived(object message)
        {
            // TODO: GET ALL MESSAGES

            OnPropertyChanged("CurrentConversation");
            return true;
        }

        public string MessageToSend { get; set; }

        public UserModel CurrentUserInfo
        {
            get
            {
                return new UserModel()
                           {
                               Id = 50001
                           };
            }
        }

        public UserModel Partner 
        { 
            get
            {
                return new UserModel()
                           {
                               Id = 50000,
                               Username = "Pesho123",
                               ProfilePictureUrl = "http://images1.wikia.nocookie.net/__cb20120325053014/mugen/images/6/6f/Spiderman.png"
                           };
            }
        }

        public ConversationModel CurrentConversation
        {
            get
            {
                return new ConversationModel()
                           {
                               Messages = new List<MessageModel>()
                                              {
                                                  new MessageModel()
                                                      {
                                                          Sender = new UserModel(){Username = "Pesho"},
                                                          Content = "Pesho kaza"
                                                      },
                                                  new MessageModel()
                                                      {
                                                          Sender = new UserModel(){Username = "Gosho"},
                                                          Content = "gosho kaza"
                                                      }
                                              }
                           };
            }
        }

        private ICommand sendMessage;
        public ICommand SendMessage
        {
            get
            {
                if(this.sendMessage == null)
                {
                    this.sendMessage = new RelayCommand(HandleSendMessageCommand);
                }

                return this.sendMessage;
            }
        }

        private void HandleSendMessageCommand(object parameter)
        {
            // TODo: SEND MESSAGE
            pubnub.Publish(channelName, "New message");
        }

        public string Name { get { return "Conversation Window"; } }
    }
}
