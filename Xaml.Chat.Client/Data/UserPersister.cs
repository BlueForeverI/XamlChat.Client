﻿namespace Xaml.Chat.Client.Data
{
    using System;
    using System.Collections.Generic;
    using Xaml.Chat.Client.Models;
        
    public class UserPersister
    {
        private static string baseUrl = "http://xamlchat.apphb.com/api/";

        public static UserModel LoginUser(string userName, string authCode)
        {
            ValidateUsername(userName);
            ValidateAuthCode(authCode);

            LogUserModel user = new LogUserModel()
            {
                Username = userName,
                PasswordHash = authCode,
            };

            UserModel loggedUser = HttpRequester.
                Post<UserModel>(baseUrl + "user/login", user);

            return loggedUser;
        }

        public static UserModel RegisterUser(RegisterUserModel newUser)
        {
            ValidateUsername(newUser.Username);
            ValidateAuthCode(newUser.PasswordHash);

            UserModel registeredUser = HttpRequester.
                Post<UserModel>(baseUrl + "user/register", newUser);

            return registeredUser;
        }

        public static void LogoutUser(string sessionKey)
        {
            IDictionary<string,string> headers = new Dictionary<string,string>();
            headers[HttpRequester.sessionKeyHeaderName]=sessionKey;
            HttpRequester.Get(baseUrl + "users/logout", headers);
        }

        public static UserModel EditUser(string sessionKey, UserEditModel editedUser)
        {
            ValidateUsername(editedUser.Username);
            ValidateAuthCode(editedUser.NewPasswordHash);

            UserModel result = HttpRequester.
                Post<UserModel>(baseUrl + "user/edit", editedUser);

            return result;
        }

        private static void ValidateUsername(string userName)
        {
            if (String.IsNullOrEmpty(userName))            
                throw new ArgumentNullException("Username must not be empty or null");            
        }

        private static void ValidateAuthCode(string authCode)
        {
            if (authCode == null || authCode.Length != 40)            
                throw new ArgumentOutOfRangeException("Authentication code must be 40 chars");            
        }
    }
}
