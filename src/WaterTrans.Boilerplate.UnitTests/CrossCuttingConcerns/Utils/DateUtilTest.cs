using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using WaterTrans.Boilerplate.CrossCuttingConcerns.ExtensionMethods;

namespace WaterTrans.Boilerplate.CrossCuttingConcerns.Utils.UnitTests
{
    [TestClass]
    public class DateUtilTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ParseISO8601Date_nullは例外()
        {
            DateUtil.ParseISO8601Date(null);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void ParseISO8601Date_解析できない場合は例外()
        {
            DateUtil.ParseISO8601Date("2000/01/01");
        }

        [TestMethod]
        public void ParseISO8601Date_解析できること()
        {
            Assert.AreEqual(new DateTime(2000, 1, 1), DateUtil.ParseISO8601Date("2000-01-01"));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ParseISO8601_nullは例外()
        {
            DateUtil.ParseISO8601(null);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void ParseISO8601_解析できない場合は例外()
        {
            DateUtil.ParseISO8601("2000/01/01T12:34:56+00:00");
        }

        [TestMethod]
        public void ParseISO8601_解析できること()
        {
            Assert.AreEqual(new DateTimeOffset(2000, 1, 1, 12, 34, 56, TimeSpan.FromHours(9)).DateTime, DateUtil.ParseISO8601("2000-01-01T12:34:56+09:00"));
        }

        [TestMethod]
        public void IsISO8601Date_スラッシュ区切りはFalse()
        {
            Assert.IsFalse(DateUtil.IsISO8601Date("2000/01/01"));
        }

        [TestMethod]
        public void IsISO8601Date_区切り文字なしはFalse()
        {
            Assert.IsFalse(DateUtil.IsISO8601Date("20000101"));
        }

        [TestMethod]
        public void IsISO8601Date_ゼロサプレスはFalse()
        {
            Assert.IsFalse(DateUtil.IsISO8601Date("2000-1-1"));
        }

        [TestMethod]
        public void IsISO8601Date_正しい表記はTrue()
        {
            Assert.IsTrue(DateUtil.IsISO8601Date("2000-01-01"));
        }

        [TestMethod]
        public void IsISO8601_スラッシュ区切りはFalse()
        {
            Assert.IsFalse(DateUtil.IsISO8601("2000/01/01T12:34:56+09:00"));
        }

        [TestMethod]
        public void IsISO8601_区切り文字なしはFalse()
        {
            Assert.IsFalse(DateUtil.IsISO8601("20000101T12:34:56+09:00"));
        }

        [TestMethod]
        public void IsISO8601_ゼロサプレスはFalse()
        {
            Assert.IsFalse(DateUtil.IsISO8601("2000-1-1T12:34:56+09:00"));
        }

        [TestMethod]
        public void IsISO8601_プラスのタイムゾーン()
        {
            Assert.IsTrue(DateUtil.IsISO8601("2000-01-01T12:34:56+09:00"));
        }

        [TestMethod]
        public void IsISO8601_UTCのタイムゾーン()
        {
            Assert.IsTrue(DateUtil.IsISO8601("2000-01-01T12:34:56+00:00"));
        }

        [TestMethod]
        public void IsISO8601_マイナスのタイムゾーン()
        {
            Assert.IsTrue(DateUtil.IsISO8601("2000-01-01T12:34:56-09:00"));
        }

        [TestMethod]
        public void IsISO8601_Zが末尾のタイムゾーンはFalse()
        {
            Assert.IsFalse(DateUtil.IsISO8601("2000-01-01T12:34:56Z"));
        }

        [TestMethod]
        public void ToISO8601Date_時刻情報なし()
        {
            DateTime? dt = new DateTime(2000, 1, 1, 0, 0, 0);
            Assert.AreEqual("2000-01-01", dt.ToISO8601Date());
        }

        [TestMethod]
        public void ToISO8601Date_Hourは無視()
        {
            DateTime? dt = new DateTime(2000, 1, 1, 12, 0, 0);
            Assert.AreEqual("2000-01-01", dt.ToISO8601Date());
        }

        [TestMethod]
        public void ToISO8601Date_Minuteは無視()
        {
            DateTime? dt = new DateTime(2000, 1, 1, 0, 30, 0);
            Assert.AreEqual("2000-01-01", dt.ToISO8601Date());
        }

        [TestMethod]
        public void ToISO8601Date_Secondは無視()
        {
            DateTime? dt = new DateTime(2000, 1, 1, 0, 0, 59);
            Assert.AreEqual("2000-01-01", dt.ToISO8601Date());
        }

        [TestMethod]
        public void ToISO8601_UTC日付()
        {
            DateTime? offset = new DateTime(2000, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            Assert.AreEqual("2000-01-01T00:00:00+00:00", offset.ToISO8601());
        }
    }
}
