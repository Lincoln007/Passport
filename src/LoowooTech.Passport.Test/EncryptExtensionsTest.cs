using LoowooTech.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace LoowooTech.Passport.Test
{
    
    [TestClass()]
    public class EncryptExtensionsTest
    {
        /// <summary>
        ///A test for AESEncrypt
        ///</summary>
        [TestMethod()]
        public void AESEncryptTest()
        {
            string str = "hello worldaaaaaaaaaaaaaaaas" ;
            var actual = str.AESEncrypt().ToHexString();
            var expected = actual.FromHexString().AESDecrypt();
            Assert.AreEqual(expected, str);

            actual = str.AESEncrypt().ToBase64String();
            expected = actual.FromBase64String().AESDecrypt();
            Assert.AreEqual(expected, str);
        }
    }
}
