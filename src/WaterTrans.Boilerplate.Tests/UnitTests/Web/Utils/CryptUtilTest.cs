using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace WaterTrans.Boilerplate.Web.UnitTests
{
    [TestClass]
    [TestCategory("UnitTests")]
    public class CryptUtilTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void HashPassword_引数passwordがNullで例外()
        {
            CryptUtil.HashPassword(null, new byte[] { }, 100);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void HashPassword_引数saltがNullで例外()
        {
            CryptUtil.HashPassword("password", null, 100);
        }

        [TestMethod]
        public void HashPassword_例外が発生しない()
        {
            byte[] salt = Encoding.Unicode.GetBytes("password");
            CryptUtil.HashPassword("password", salt, 100);
        }

        [TestMethod]
        public void HashPassword_引数iterationsが違う場合にHash値は一致しない()
        {
            byte[] salt = Encoding.Unicode.GetBytes("password");
            var dic = new Dictionary<string, string>();

            for (int i = 10; i < 20; i++)
            {
                dic.Add(Encoding.Unicode.GetString(CryptUtil.HashPassword("password", salt, i)), i.ToString());
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void VerifyPassword_引数passwordがNullで例外()
        {
            CryptUtil.VerifyPassword(null, new byte[] { }, 100, new byte[] { });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void VerifyPassword_引数saltがNullで例外()
        {
            CryptUtil.VerifyPassword("password", null, 100, new byte[] { });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void VerifyPassword_引数hashedPasswordがNullで例外()
        {
            CryptUtil.VerifyPassword("password", null, 100, null);
        }

        [TestMethod]
        public void VerifyPassword_例外が発生しない()
        {
            byte[] salt = Encoding.Unicode.GetBytes("password");
            CryptUtil.VerifyPassword("password", salt, 100, new byte[] { });
        }

        [TestMethod]
        public void VerifyPassword_引数iterationsが変化してもすべて一致する()
        {
            byte[] salt = Encoding.Unicode.GetBytes("password");
            Assert.IsTrue(CryptUtil.VerifyPassword("password", salt, 100, CryptUtil.HashPassword("password", salt, 100)));
            Assert.IsTrue(CryptUtil.VerifyPassword("password", salt, 105, CryptUtil.HashPassword("password", salt, 105)));
            Assert.IsTrue(CryptUtil.VerifyPassword("password", salt, 110, CryptUtil.HashPassword("password", salt, 110)));
        }

        [TestMethod]
        public void VerifyPassword_引数saltが変化してもすべて一致する()
        {
            byte[] salt = Encoding.Unicode.GetBytes("hogehoge");
            Assert.IsTrue(CryptUtil.VerifyPassword("password", salt, 100, CryptUtil.HashPassword("password", salt, 100)));
            Assert.IsTrue(CryptUtil.VerifyPassword("password", salt, 105, CryptUtil.HashPassword("password", salt, 105)));
            Assert.IsTrue(CryptUtil.VerifyPassword("password", salt, 110, CryptUtil.HashPassword("password", salt, 110)));
        }

        [TestMethod]
        public void HashPassword_管理者の初期パスワード()
        {
            string password = "admin-secret";
            int iterations = 1000;
            byte[] salt = Guid.Empty.ToByteArray().Concat(Guid.Empty.ToByteArray()).ToArray();
            byte[] hashedPassword = CryptUtil.HashPassword(password, salt, iterations);
            Debug.WriteLine("0x" + BitConverter.ToString(salt).Replace("-", string.Empty));
            Debug.WriteLine("0x" + BitConverter.ToString(hashedPassword).Replace("-", string.Empty));
        }
    }
}