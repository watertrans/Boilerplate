using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using WaterTrans.Boilerplate.Infrastructure.Cryptography;

namespace WaterTrans.Boilerplate.Web.UnitTests
{
    [TestClass]
    [TestCategory("UnitTests")]
    public class PasswordHashProviderTest
    {
        private PasswordHashProvider _passwordHashProvider = new PasswordHashProvider();

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Hash_引数passwordがNullで例外()
        {
            _passwordHashProvider.Hash(null, new byte[] { }, 100);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Hash_引数saltがNullで例外()
        {
            _passwordHashProvider.Hash("password", null, 100);
        }

        [TestMethod]
        public void Hash_例外が発生しない()
        {
            byte[] salt = Encoding.Unicode.GetBytes("password");
            _passwordHashProvider.Hash("password", salt, 100);
        }

        [TestMethod]
        public void Hash_引数iterationsが違う場合にHash値は一致しない()
        {
            byte[] salt = Encoding.Unicode.GetBytes("password");
            var dic = new Dictionary<string, string>();

            for (int i = 10; i < 20; i++)
            {
                dic.Add(Encoding.Unicode.GetString(_passwordHashProvider.Hash("password", salt, i)), i.ToString());
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Verify_引数passwordがNullで例外()
        {
            _passwordHashProvider.Verify(null, new byte[] { }, 100, new byte[] { });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Verify_引数saltがNullで例外()
        {
            _passwordHashProvider.Verify("password", null, 100, new byte[] { });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Verify_引数hashedPasswordがNullで例外()
        {
            _passwordHashProvider.Verify("password", null, 100, null);
        }

        [TestMethod]
        public void Verify_例外が発生しない()
        {
            byte[] salt = Encoding.Unicode.GetBytes("password");
            _passwordHashProvider.Verify("password", salt, 100, new byte[] { });
        }

        [TestMethod]
        public void Verify_引数iterationsが変化してもすべて一致する()
        {
            byte[] salt = Encoding.Unicode.GetBytes("password");
            Assert.IsTrue(_passwordHashProvider.Verify("password", salt, 100, _passwordHashProvider.Hash("password", salt, 100)));
            Assert.IsTrue(_passwordHashProvider.Verify("password", salt, 105, _passwordHashProvider.Hash("password", salt, 105)));
            Assert.IsTrue(_passwordHashProvider.Verify("password", salt, 110, _passwordHashProvider.Hash("password", salt, 110)));
        }

        [TestMethod]
        public void Verify_引数saltが変化してもすべて一致する()
        {
            byte[] salt = Encoding.Unicode.GetBytes("hogehoge");
            Assert.IsTrue(_passwordHashProvider.Verify("password", salt, 100, _passwordHashProvider.Hash("password", salt, 100)));
            Assert.IsTrue(_passwordHashProvider.Verify("password", salt, 105, _passwordHashProvider.Hash("password", salt, 105)));
            Assert.IsTrue(_passwordHashProvider.Verify("password", salt, 110, _passwordHashProvider.Hash("password", salt, 110)));
        }

        [TestMethod]
        public void Hash_管理者の初期パスワード()
        {
            string password = "admin-secret";
            int iterations = 1000;
            byte[] salt = Guid.Empty.ToByteArray().Concat(Guid.Empty.ToByteArray()).ToArray();
            byte[] hashedPassword = _passwordHashProvider.Hash(password, salt, iterations);
            Debug.WriteLine("0x" + BitConverter.ToString(salt).Replace("-", string.Empty));
            Debug.WriteLine("0x" + BitConverter.ToString(hashedPassword).Replace("-", string.Empty));
        }

        [TestMethod]
        public void Hash_停止中管理者の初期パスワード()
        {
            string password = "suspended-secret";
            int iterations = 1000;
            byte[] salt = Guid.Empty.ToByteArray().Concat(Guid.Empty.ToByteArray()).ToArray();
            byte[] hashedPassword = _passwordHashProvider.Hash(password, salt, iterations);
            Debug.WriteLine("0x" + BitConverter.ToString(salt).Replace("-", string.Empty));
            Debug.WriteLine("0x" + BitConverter.ToString(hashedPassword).Replace("-", string.Empty));
        }

        [TestMethod]
        public void Hash_Readerの初期パスワード()
        {
            string password = "reader-secret";
            int iterations = 1000;
            byte[] salt = Guid.Empty.ToByteArray().Concat(Guid.Empty.ToByteArray()).ToArray();
            byte[] hashedPassword = _passwordHashProvider.Hash(password, salt, iterations);
            Debug.WriteLine("0x" + BitConverter.ToString(salt).Replace("-", string.Empty));
            Debug.WriteLine("0x" + BitConverter.ToString(hashedPassword).Replace("-", string.Empty));
        }
    }
}