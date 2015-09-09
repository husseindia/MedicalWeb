using System;
using System.Collections.Generic;
using System.Linq;
using MedicalWeb.Models;
using OAuth2.Mvc;

namespace MedicalWeb.OAuth
{
    public class DemoService : OAuthServiceBase
    {

        private PharmciRepositories db = new PharmciRepositories();

        static DemoService()
        {
            Tokens = new List<DemoToken>();
            RequestTokens = new Dictionary<string, DateTime>();
        }

        public static List<DemoToken> Tokens { get; set; }

        public static Dictionary<String, DateTime> RequestTokens { get; set; }

        public override OAuthResponse RequestToken()
        {
            var token = Guid.NewGuid().ToString("N");
            var expire = DateTime.Now.AddMinutes(5);
            RequestTokens.Add(token, expire);

            return new OAuthResponse
                   {
                       Expires = (int)expire.Subtract(DateTime.Now).TotalSeconds,
                       RequestToken = token,
                       RequireSsl = false,
                       Success = true
                   };
        }

        public override OAuthResponse AccessToken(string requestToken, string grantType, string userName, string password, bool persistent)
        {
            // This should go out to a DB and get the users saved information.
            //var hash = (requestToken + userName).ToSHA1();

            try
            {
                var user = db.GetByUserName(userName);

                if (user != null)
                    if (user.VerifyPassword(password))
                        return CreateAccessToken(user);

            }
            catch (Exception)
            {
                return new OAuthResponse
                {
                    Error = "The username or password was not correct.",
                    Success = false
                };
            }
            return new OAuthResponse
            {
                Error = "The username or password was not correct.",
                Success = false
            };
        }

        public override OAuthResponse RefreshToken(string refreshToken)
        {
            var token = Tokens.FirstOrDefault(t => t.RefreshToken == refreshToken);

            if (token == null)
                return new OAuthResponse
                       {
                           Error = "RefreshToken not found.",
                           Success = false
                       };

            if (token.IsRefreshExpired)
                return new OAuthResponse
                       {
                           Error = "RefreshToken expired.",
                           Success = false
                       };

            Tokens.Remove(token);
            return CreateAccessToken(token.Name);
        }

        private OAuthResponse CreateAccessToken(Pharmci donor)
        {
            var token = new DemoToken(donor.UserName, donor.Id);
            Tokens.Add(token);

            return new OAuthResponse
            {
                DonorId = donor.Id,
                BloodType = donor.BloodType,
                IsFirstTimeDonor =  donor.IsFirstTimeDonor,
                LastDonationDate = donor.LastDonationDate,
                AccessToken = token.AccessToken,
                Expires = token.ExpireSeconds,
                RefreshToken = token.RefreshToken,
                RequireSsl = false,
                Success = true
            };
        }

        private OAuthResponse CreateAccessToken(string name)
        {
            var user = db.GetByUserName(name);

            if (user != null)
                return CreateAccessToken(user);
            return new OAuthResponse
            {
                Error = "The username not active right now.",
                Success = false
            };

        }

        public override bool UnauthorizeToken(string accessToken)
        {
            var token = Tokens.FirstOrDefault(t => t.AccessToken == accessToken);
            if (token == null)
                return false;

            Tokens.Remove(token);
            return true;
        }
    }
}