using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xaml.Chat.Client.Models;

namespace Xaml.Chat.Client.ViewModels
{
    public class ConversationViewModel : ViewModelBase, IPageViewModel
    {
        public UserModel Partner 
        { 
            get
            {
                return new UserModel()
                           {
                               Username = "Pesho123",
                               ProfilePictureUrl = "http://images1.wikia.nocookie.net/__cb20120325053014/mugen/images/6/6f/Spiderman.png"
                           };
            }
        }

        public string Name { get { return "Conversation Window"; } }
    }
}
