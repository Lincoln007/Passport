using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LoowooTech.Passport.Model;
using LoowooTech.Passport.Common;
using LoowooTech.Passport.Dao;


namespace LoowooTech.Passport.Manager
{
    public class AuthManager : ManagerBase
    {
        public AuthManager(Core core) : base(core) { }

        private static ConcurrentDictionary<string, AuthorizeCode> _codes = new ConcurrentDictionary<string, AuthorizeCode>();

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
            var dao = new AuthDao();
            return dao.GetAccessToken(code.ClientId, code.AccountId);
        }

        //public int GetAccountId(string accessToken)
        //{
        //    var code = _tokens[accessToken];
        //    if (code == null)
        //    {
        //        return new AuthDao().GetAccountId(accessToken);
        //    }
        //    return code.AccountId;
        //}
    }
}