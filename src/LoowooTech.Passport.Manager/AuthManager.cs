using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LoowooTech.Passport.Model;
using LoowooTech.Common;
using LoowooTech.Passport.Dao;


namespace LoowooTech.Passport.Manager
{
    public class AuthManager : ManagerBase
    {
        private string codeHashId = "authorize_code";
        private string tokenHashId = "access_token";

        public string GenerateCode(Client client, Account account)
        {
            var code = DateTime.Now.Ticks.ToString().MD5();
            Cache.HSet(codeHashId, code, new AuthorizeCode
            {
                ClientId = client.ClientId,
                AccountId = account.AccountId,
                AgentId = account.AgentId
            });
            return code;
        }

        public string GetAppendedCodeReturnUrl(Client client, Account account, string returnUrl)
        {
            var code = GenerateCode(client, account);
            if (!returnUrl.Contains("?"))
            {
                returnUrl += "?";
            }
            returnUrl += "&code=" + code;
            return returnUrl;
        }

        public AuthorizeCode GetAuthorizeCode(string code)
        {
            var authCode = Cache.HGet<AuthorizeCode>(codeHashId, code);
            if (authCode == null) return null;

            var expired = (DateTime.Now - authCode.CreateTime).TotalMinutes > 10;
            if (expired) return null;
            Cache.HRemove(codeHashId, code);

            return authCode;
        }

        private string GetAccessTokenCacheKey(AuthorizeCode code)
        {
            return code.AccountId.ToString() + "_" + code.AgentId.ToString();
        }

        public AccessToken GetAccessToken(AuthorizeCode code)
        {
            var key = GetAccessTokenCacheKey(code);
            var accessToken = Cache.HGet<AccessToken>(tokenHashId, key);
            if (accessToken == null)
            {
                accessToken = new AccessToken
                {
                    AccountId = code.AccountId,
                    AgentId = code.AgentId,
                    Token = AccessToken.GenerateToken(code)
                };
                Cache.HSet(tokenHashId, key, accessToken);
            }
            return accessToken;
        }
    }
}