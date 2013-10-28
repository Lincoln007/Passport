using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LoowooTech.Passport.Common;

namespace LoowooTech.Passport.Model
{
    public class AccessToken
    {
        public int ID { get; set; }

        public string Token { get; set; }

        public DateTime CreateTime { get; set; }

        public int AccountId { get; set; }

        public string ClientId { get; set; }

        public int AgentId { get; set; }

        public string GenerateToken()
        {
            return AccessToken.GenerateToken(ClientId, AccountId, CreateTime);
        }

        public static string GenerateToken(string clientId, int accountId, DateTime createTime)
        {
            return (clientId + "|" + accountId + "|" + createTime.ToString()).AESEncrypt().ToHexString();
        }

        public static AccessToken Create(string accessToken)
        {
            var vals = accessToken.FromHexString().AESDecrypt().Split('|');
            try
            {
                return new AccessToken
                {
                    Token = accessToken,
                    ClientId = vals[0],
                    AccountId = int.Parse(vals[1]),
                    CreateTime = DateTime.Parse(vals[2])
                };
            }
            catch
            {
                throw new ArgumentException("invalid access_token");
            }
        }
    }
}