using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using WaterTrans.Boilerplate.Domain.Utils;

namespace WaterTrans.Boilerplate.Tests.UnitTests.Domain.Utils
{
    [TestClass]
    [TestCategory("UnitTests")]
    public class JsonUtilTest
    {
        [TestMethod]
        public void ToRawJsonArray_単一の文字列の配列が返ること()
        {
            string expected = "[\"String\"]";
            string actual = JsonUtil.ToRawJsonArray("[{\"Value\":\"String\"}]", "Value");
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ToRawJsonArray_複数の文字列の配列が返ること()
        {
            string expected = "[\"String1\",\"String2\"]";
            string actual = JsonUtil.ToRawJsonArray("[{\"Value\":\"String1\"},{\"Value\":\"String2\"}]", "Value");
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ToRawJsonArray_単一の数値の配列が返ること()
        {
            string expected = "[1]";
            string actual = JsonUtil.ToRawJsonArray("[{\"Value\":1}]", "Value");
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ToRawJsonArray_複数の数値の配列が返ること()
        {
            string expected = "[1,2]";
            string actual = JsonUtil.ToRawJsonArray("[{\"Value\":1},{\"Value\":2}]", "Value");
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ToRawJsonArray_空文字は空配列()
        {
            string expected = "[]";
            string actual = JsonUtil.ToRawJsonArray("", "Value");
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ToRawJsonArray_nullは空配列()
        {
            string expected = "[]";
            string actual = JsonUtil.ToRawJsonArray(null, "Value");
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ToRawJsonArray_空配列は空配列()
        {
            string expected = "[]";
            string actual = JsonUtil.ToRawJsonArray("[]", "Value");
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ToRawJsonArray_存在しないキーは空配列()
        {
            string expected = "[]";
            string actual = JsonUtil.ToRawJsonArray("[{\"Value\":true}]", "Error");
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ToRawJsonArray_単一のブール値の配列が返ること()
        {
            string expected = "[true]";
            string actual = JsonUtil.ToRawJsonArray("[{\"Value\":true}]", "Value");
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ToRawJsonArray_複数のブール値の配列が返ること()
        {
            string expected = "[true,false]";
            string actual = JsonUtil.ToRawJsonArray("[{\"Value\":true},{\"Value\":false}]", "Value");
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Serialize_nullはnullに変換されること()
        {
            string expected = null;
            dynamic value = null;
            string actual = JsonUtil.Serialize(value);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Serialize_日本語が文字参照にならないこと()
        {
            string expected = "{\"value\":\"日本語\"}";
            dynamic value = new { Value = "日本語" };
            string actual = JsonUtil.Serialize(value);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Serialize_プロパティ名がキャメルケースになること()
        {
            dynamic value = new { MyProperty = "Value" };
            string expected = "{\"myProperty\":\"Value\"}";
            string actual = JsonUtil.Serialize(value);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Serialize_ディクショナリのキー名がキャメルケースになること()
        {
            var dic = new Dictionary<string, object>();
            dic["KeyName1"] = "Value";
            dic["KeyName2"] = "Value";
            string expected = "{\"myDictionary\":{\"keyName1\":\"Value\",\"keyName2\":\"Value\"}}";
            dynamic value = new { MyDictionary = dic };
            string actual = JsonUtil.Serialize(value);
            Assert.AreEqual(expected, actual);
        }

        private enum TestEnum
        {
            YES,
            NO,
            UNKNOWN
        }

        [TestMethod]
        public void Serialize_Enumの値が文字列になること()
        {
            string expected = "{\"value1\":\"YES\",\"value2\":\"UNKNOWN\"}";
            dynamic value = new { Value1 = TestEnum.YES, Value2 = TestEnum.UNKNOWN };
            string actual = JsonUtil.Serialize(value);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Deserialize_nullはnullに変換されること()
        {
            string value = null;
            DeserializeTestClass1 actual = JsonUtil.Deserialize<DeserializeTestClass1>(value);
            Assert.IsNull(actual);
        }

        [TestMethod]
        public void Deserialize_空文字はnullに変換されること()
        {
            string value = string.Empty;
            DeserializeTestClass1 actual = JsonUtil.Deserialize<DeserializeTestClass1>(value);
            Assert.IsNull(actual);
        }

        [TestMethod]
        public void Deserialize_日本語が読み取りできることかつキャメルケースでマッピングできること()
        {
            DeserializeTestClass1 expected = new DeserializeTestClass1 { Value = "日本語" };
            string value = "{\"value\":\"日本語\"}";
            DeserializeTestClass1 actual = JsonUtil.Deserialize<DeserializeTestClass1>(value);
            Assert.AreEqual(expected.Value, actual.Value);
        }

        private class DeserializeTestClass1
        {
            public string Value { get; set; }
        }

        [TestMethod]
        public void Deserialize_ディクショナリのキー名を読み取り可能であること()
        {
            string value = "{\"myDictionary\":{\"keyName1\":\"Value1\",\"keyName2\":\"Value2\"}}";
            DeserializeTestClass2 actual = JsonUtil.Deserialize<DeserializeTestClass2>(value);
            Assert.AreEqual("Value1", actual.MyDictionary["keyName1"].ToString());
            Assert.AreEqual("Value2", actual.MyDictionary["keyName2"].ToString());
        }

        private class DeserializeTestClass2
        {
            public Dictionary<string, object> MyDictionary { get; set; }
        }

        [TestMethod]
        public void Deserialize_Enumの文字列値が読み込めること()
        {
            string value = "{\"enumValue\":\"Yes\", \"enumValue2\":\"NO\"}";
            DeserializeTestClass3 actual = JsonUtil.Deserialize<DeserializeTestClass3>(value);
            Assert.AreEqual(TestEnum.YES, actual.EnumValue);
            Assert.AreEqual(TestEnum.NO, actual.EnumValue2);
        }

        private class DeserializeTestClass3
        {
            public TestEnum EnumValue { get; set; }
            public TestEnum EnumValue2 { get; set; }
        }
    }
}

