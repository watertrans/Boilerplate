using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WaterTrans.Boilerplate.CrossCuttingConcerns.ExtensionMethods;

namespace WaterTrans.Boilerplate.CrossCuttingConcerns.Utils.UnitTests
{
    [TestClass]
    [TestCategory("UnitTests")]
    public class StringUtilTest
    {
        [TestMethod]
        public void ToCamelCase_nullはnullを返すこと()
        {
            string acutual = null;
            Assert.AreEqual(null, acutual.ToCamelCase());
        }

        [TestMethod]
        public void ToCamelCase_空文字は空文字を返すこと()
        {
            string acutual = string.Empty;
            Assert.AreEqual(string.Empty, acutual.ToCamelCase());
        }

        [TestMethod]
        public void ToCamelCase_先頭の大文字は小文字に変換されること()
        {
            string acutual = "Goods";
            Assert.AreEqual("goods", acutual.ToCamelCase());
        }

        [TestMethod]
        public void ToCamelCase_先頭以外の大文字は小文字に変換されないこと()
        {
            string acutual = "GOODS";
            Assert.AreEqual("gOODS", acutual.ToCamelCase());
        }

        [TestMethod]
        public void ToCamelCase_先頭の日本語は変換されないこと()
        {
            string acutual = "日本語";
            Assert.AreEqual("日本語", acutual.ToCamelCase());
        }

        [TestMethod]
        public void CreateCode_連続して実行した結果が44文字以内ですべて重複しないこと()
        {
            var hash = new HashSet<string>();
            for (int i = 0; i < 20; i++)
            {
                string item = StringUtil.CreateCode();
                Assert.IsTrue(item.Length <= 44);
                Assert.IsTrue(hash.Add(item));
            }
        }

        [TestMethod]
        public void Base64UrlEncode_変換結果が一致すること()
        {
            byte[] original = Guid.NewGuid().ToByteArray();
            string encoded = StringUtil.Base64UrlEncode(original);
            byte[] decoded = StringUtil.Base64UrlDecode(encoded);
            Assert.AreEqual(original.ToString(), decoded.ToString());
        }

        [TestMethod]
        public void Base64UrlDecode_変換結果が一致すること()
        {
            Assert.IsTrue(Encoding.ASCII.GetBytes("0123456789").SequenceEqual(StringUtil.Base64UrlDecode("MDEyMzQ1Njc4OQ")));
            Assert.IsTrue(Encoding.ASCII.GetBytes("0123456789A").SequenceEqual(StringUtil.Base64UrlDecode("MDEyMzQ1Njc4OUE")));
            Assert.IsTrue(Encoding.ASCII.GetBytes("0123456789AB").SequenceEqual(StringUtil.Base64UrlDecode("MDEyMzQ1Njc4OUFC")));
        }
    }
}

