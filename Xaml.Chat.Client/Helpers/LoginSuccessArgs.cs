using System;

namespace Xaml.Chat.Client.Helpers
{
    public class LoginSuccessArgs:EventArgs
    {
        public string Username { get; set; }

        public LoginSuccessArgs(string username)
            : base()
        {
            this.Username = username;
        }
    }
}
