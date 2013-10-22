using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;
using System.IO;

namespace LoowooTech.Passport.Common
{
    public static class EncryptExtensions
    {
        private static System.Security.Cryptography.MD5 _md5Hash = System.Security.Cryptography.MD5.Create();

        public static string MD5(this string str)
        {
            var data = _md5Hash.ComputeHash(System.Text.Encoding.UTF8.GetBytes(str));
            var sb = new StringBuilder();
            foreach (var b in data)
            {
                sb.Append(b.ToString("x2"));
            }
            return sb.ToString();
        }

        private static System.Security.Cryptography.SHA1 _sha1 = System.Security.Cryptography.SHA1.Create();
        public static string SHA1(this string str)
        {
            var data = _sha1.ComputeHash(System.Text.Encoding.UTF8.GetBytes(str));
            var sb = new StringBuilder();
            foreach (var b in data)
            {
                sb.Append(b.ToString("x2"));
            }
            return sb.ToString();
        }

        private static byte[] AES_KEY;
        private static byte[] AES_IV;
        static EncryptExtensions()
        {
            AES_KEY = Encoding.UTF8.GetBytes("lwaeskey");
            AES_IV = AES_KEY;
        }

        public static string AESEncrypt(this string str)
        {
            using (var aes = System.Security.Cryptography.Aes.Create())
            {
                aes.Key = AES_KEY;
                aes.IV = AES_IV;

                var encryptor = aes.CreateEncryptor();
                using (var ms = new MemoryStream())
                {
                    using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        using (var sw = new StreamWriter(cs))
                        {
                            sw.Write(str);
                            return Encoding.UTF8.GetString(ms.ToArray());
                        }
                    }
                }
            }
        }

        public static string AESDecrypt(this string str)
        {
            using (var aes = System.Security.Cryptography.Aes.Create())
            {
                aes.Key = AES_KEY;
                aes.IV = AES_IV;

                var decryptor = aes.CreateDecryptor();
                using (var ms = new MemoryStream())
                {
                    using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    {
                        using (var sr = new StreamReader(cs))
                        {
                            return sr.ReadToEnd();
                        }
                    }
                }
            }
        }
    }
}