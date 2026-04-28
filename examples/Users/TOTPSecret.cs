using CommunityToolkit.Diagnostics;
using CSharpFunctionalExtensions;
using Hospital.proj.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.proj.Domain.Users
{
    public class TOTPSecret : Entity
    {
        public string Secret { get; set; }
        public User User { get; set; }
        private static readonly char[] chars =
"ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();

        private TOTPSecret(string secret, User user)
        {
            Secret = secret;
            User = user;
        }

        //public static void CreateSecretForUser(string secretName, User user, out string keyUri)
        //{
        //    Guard.IsNotNull(secretName);
        //    Guard.IsNotNull(user);

        //    var token = RandomNumberGenerator.GetBytes(64);

        //    var result = new StringBuilder();
        //    for (var i = 0; i < 16; i++)
        //    {
        //        var rnd = BitConverter.ToInt32(token, i + 4);
        //        var index = rnd % chars.Length;
        //        result.Append(chars[index]);
        //    }

        //    var secretValue = result.ToString();

        //    var userSecret = new UserSecret(secretValue, secretName, user);
        //    user.AddSecret(userSecret);

        //    keyUri = string.Format(
        //        "otpauth://totp/{0}: {1}?secret={2}&issuer={0}",
        //        WebUtility.UrlEncode("Hospital"), WebUtility.UrlEncode(user.Email.Value),
        //        userSecret.Secret);
        //}

    }

    public static class UserSecretNames 
    {
        public const string TOTP = "TOTP";
        public const string PasswordChange = "PasswordChange";

    }
}
