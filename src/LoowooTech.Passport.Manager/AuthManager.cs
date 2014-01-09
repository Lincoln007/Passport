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
        private static ConcurrentDictionary<string, AuthorizeCode> _codes = new ConcurrentDictionary<string, AuthorizeCode>();
        private static readonly AuthDao Dao = new AuthDao();

        public string GenerateCode(Client client, int accountId)
        {
            var code = DateTime.Now.Ticks.ToString().MD5();
            _codes.TryAdd(code, new AuthorizeCode
            {
                ClientId = client.ClientId,
                AccountId = accountId,
                CreateTime = DateTime.Now,
            });
            return code;
        }

        public AuthorizeCode GetAuthorizeCode(string code)
        {
            if (!_codes.ContainsKey(code)) return null;

            var authCode = _codes[code];

            var expired = (DateTime.Now - authCode.CreateTime).TotalMinutes > 10;
            if (expired) return null;

            _codes.TryRemove(code, out authCode);

            return authCode;
        }

        public AccessToken GetAccessToken(AuthorizeCode code)
        {
            return Dao.GetAccessToken(code.ClientId, code.AccountId);
        }
    }
}