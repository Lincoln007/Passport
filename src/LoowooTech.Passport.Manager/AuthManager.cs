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

        private static ConcurrentDictionary<string, AuthCode> _codes = new ConcurrentDictionary<string, AuthCode>();
        private static ConcurrentDictionary<string, AuthCode> _tokens = new ConcurrentDictionary<string, AuthCode>();

        public string GenerateCode(Client client, int accountId)
        {
            var code = Guid.NewGuid().ToString().MD5();
            _codes.TryAdd(code, new AuthCode
            {
                ClientId = client.ClientId,
                AccountId = accountId,
                CreateTime = DateTime.Now,
            });
            return code;
        }

        public AuthCode GetAuthCode(string code)
        {
            if (!_codes.ContainsKey(code)) return null;

            var authCode = _codes[code];

            var expired = (DateTime.Now - authCode.CreateTime).TotalMinutes > 10;
            if (expired) return null;

            _codes.TryRemove(code, out authCode);

            return authCode;
        }

        public string GetAccessToken(AuthCode authCode)
        {
            if (!string.IsNullOrEmpty(authCode.AccessToken))
            {
                return authCode.AccessToken;
            }

            var dao = new AuthDao();
            var token = dao.GetAccessToken(authCode.ClientId, authCode.AccountId);
            authCode.AccessToken = token;

            if (!_tokens.ContainsKey(token))
            {
                _tokens.TryAdd(token, authCode);
            }

            return token;
        }

        public int GetAccountId(string accessToken)
        {
            var code = _tokens[accessToken];
            if (code == null)
            {
                return new AuthDao().GetAccountId(accessToken);
            }
            return code.AccountId;
        }
    }
}