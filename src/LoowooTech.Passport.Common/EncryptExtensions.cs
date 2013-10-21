using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;

namespace LoowooTech.Passport.Common
{
    public static class EncryptExtensions
    {
        private static System.Security.Cryptography.MD5 _md5Hash = System.Security.Cryptography.MD5.Create();

        public static string MD5(this string str)
        {
            var data = _md5Hash.ComputeHash(System.Text.Encoding.UTF8.GetBytes(str));
            return System.Text.Encoding.UTF8.GetString(data);
        }

        private static System.Security.Cryptography.SHA1 _sha1 = System.Security.Cryptography.SHA1.Create();
        public static string SHA1(this string str)
        {
            var data = _sha1.ComputeHash(System.Text.Encoding.UTF8.GetBytes(str));
            return System.Text.Encoding.UTF8.GetString(data);
        }
    }
}