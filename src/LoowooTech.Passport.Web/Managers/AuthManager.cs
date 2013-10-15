using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LoowooTech.Passport.Web.Models;


namespace LoowooTech.Passport.Web.Managers
{
    public class AuthManager
    {
        private static ConcurrentDictionary<string, AuthCode> _codes = new ConcurrentDictionary<string, AuthCode>();

        public string GenerateCode(Client client, Account account)
        {
            var code = Guid.NewGuid().ToString().MD5();
            _codes.TryAdd(code, new AuthCode
            {
                ClientId = client.ClientId,
                AccountId = account.ID,
                CreateTime = DateTime.Now
            });
            return code;
        }

        public AuthCode GetClientIdByCode(string code)
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
            //get access token from db
            throw new NotImplementedException();
        }
    }
}