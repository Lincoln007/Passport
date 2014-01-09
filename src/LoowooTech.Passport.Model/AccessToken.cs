using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LoowooTech.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoowooTech.Passport.Model
{
    [Table("AUTH_TOKEN")]
    public class AccessToken
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Column("TOKEN")]
        public string Token { get; set; }

        [Column("CREATE_TIME")]
        public DateTime CreateTime { get; set; }

        [Column("ACCOUNT_ID")]
        public int AccountId { get; set; }

        [Column("CLIENT_ID")]
        public string ClientId { get; set; }

        [NotMapped]
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