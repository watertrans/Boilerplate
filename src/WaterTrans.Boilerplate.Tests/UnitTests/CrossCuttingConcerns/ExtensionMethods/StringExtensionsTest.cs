using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WaterTrans.Boilerplate.CrossCuttingConcerns.ExtensionMethods.UnitTests
{
    [TestClass]
    [TestCategory("UnitTests")]
    public class StringExtensionsTest
    {
        [TestMethod]
        public void Left_null値はそのまま返却する()
        {
            Assert.AreEqual(null, StringExtensions.Left(null, 0));
        }

        [TestMethod]
        public void Left_空文字はそのまま返却する()
        {
            Assert.AreEqual(string.Empty, StringExtensions.Left(string.Empty, 0));
        }

        [TestMethod]
        public void Left_0を指定すると空文字を返却する()
        {
            Assert.AreEqual(string.Empty, StringExtensions.Left("hoge", 0));
        }

        [TestMethod]
        public void Left_1を指定すると先頭文字を返却する()
        {
            Assert.AreEqual("h", StringExtensions.Left("hoge", 1));
        }

        [TestMethod]
        public void Left_文字数をオーバーするとすべての文字を返却する()
        {
            Assert.AreEqual("hoge", StringExtensions.Left("hoge", 5));
        }

        [TestMethod]
        public void Right_null値はそのまま返却する()
        {
            Assert.AreEqual(null, StringExtensions.Right(null, 0));
        }

        [TestMethod]
        public void Right_空文字はそのまま返却する()
        {
            Assert.AreEqual(string.Empty, StringExtensions.Right(string.Empty, 0));
        }

        [TestMethod]
        public void Right_0を指定すると空文字を返却する()
        {
            Assert.AreEqual(string.Empty, StringExtensions.Right("hoge", 0));
        }

        [TestMethod]
        public void Right_1を指定すると行末文字を返却する()
        {
            Assert.AreEqual("e", StringExtensions.Right("hoge", 1));
        }

        [TestMethod]
        public void Right_文字数をオーバーするとすべての文字を返却する()
        {
            Assert.AreEqual("hoge", StringExtensions.Right("hoge", 5));
        }

        [TestMethod]
        public void EqualsIgnoreCase_nullと比較でFalseを返す()
        {
            Assert.AreEqual(false, StringExtensions.EqualsIgnoreCase(null, string.Empty));
        }

        [TestMethod]
        public void EqualsIgnoreCase_完全一致でTrueを返す()
        {
            Assert.AreEqual(true, StringExtensions.EqualsIgnoreCase("hoge", "hoge"));
        }

        [TestMethod]
        public void EqualsIgnoreCase_一致でTrueを返す()
        {
            Assert.AreEqual(true, StringExtensions.EqualsIgnoreCase("hoge", "HOGE"));
        }

        [TestMethod]
        public void ToCamelCase_nullはそのまま返す()
        {
            Assert.AreEqual(null, StringExtensions.ToCamelCase(null));
        }

        [TestMethod]
        public void ToCamelCase_空文字はそのまま返す()
        {
            Assert.AreEqual(string.Empty, StringExtensions.ToCamelCase(string.Empty));
        }

        [TestMethod]
        public void ToCamelCase_先頭文字のみ小文字にして返す()
        {
            Assert.AreEqual("hOGE", StringExtensions.ToCamelCase("HOGE"));
        }

        [TestMethod]
        public void ToCamelCase_小文字の場合はそのまま返す()
        {
            Assert.AreEqual("hoge", StringExtensions.ToCamelCase("hoge"));
        }
    }
}