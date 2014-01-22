using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LoowooTech.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoowooTech.Passport.Model
{
    public class AccessToken
    {
        public string Token { get; set; }

        public DateTime CreateTime { get; set; }

        public int AccountId { get; set; }

        public string ClientId { get; set; }

        public int AgentId { get; set; }


        public static string GenerateToken(AuthorizeCode code)
        {
            return (code.ClientId + "|" + code.AccountId + "|" + code.AgentId + "|" + code.CreateTime).AESEncrypt().ToHexString();
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