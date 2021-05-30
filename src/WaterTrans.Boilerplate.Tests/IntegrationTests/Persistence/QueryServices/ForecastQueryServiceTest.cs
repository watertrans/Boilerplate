using Microsoft.VisualStudio.TestTools.UnitTesting;
using WaterTrans.Boilerplate.Domain;
using WaterTrans.Boilerplate.Persistence.QueryServices;

namespace WaterTrans.Boilerplate.Tests.IntegrationTests.Persistence.QueryServices
{
    [TestClass]
    [TestCategory("IntegrationTests")]
    public class ForecastQueryServiceTest
    {
        [TestMethod]
        public void Query_存在しないIDで結果がゼロ件()
        {
            var forecastQueryService = new ForecastQueryService(TestEnvironment.DBSettings);
            var pagingQuery = new PagingQuery { Page = 1, PageSize = 20 };
            var sortOrder = SortOrder.Parse(string.Empty);
            var queryResult = forecastQueryService.Query("00000000-0000-0000-0000-000000000000", sortOrder, pagingQuery);
            Assert.IsTrue(queryResult.Count == 0);
        }

        [TestMethod]
        public void Query_プライマリーキーで取得できる()
        {
            var forecastQueryService = new ForecastQueryService(TestEnvironment.DBSettings);
            var pagingQuery = new PagingQuery { Page = 1, PageSize = 20 };
            var sortOrder = SortOrder.Parse(string.Empty);
            var queryResult = forecastQueryService.Query("00000000-0000-0000-0000-000000000001", sortOrder, pagingQuery);
            Assert.IsTrue(queryResult.Count == 1);
        }

        [TestMethod]
        public void Query_存在しないページを指定して結果がゼロ件()
        {
            var forecastQueryService = new ForecastQueryService(TestEnvironment.DBSettings);
            var pagingQuery = new PagingQuery { Page = 999, PageSize = 20 };
            var sortOrder = SortOrder.Parse(string.Empty);
            var queryResult = forecastQueryService.Query("00000000-0000-0000-0000-000000000001", sortOrder, pagingQuery);
            Assert.IsTrue(queryResult.Count == 0);
        }

        [TestMethod]
        public void Query_国コードで取得できる()
        {
            var forecastQueryService = new ForecastQueryService(TestEnvironment.DBSettings);
            var pagingQuery = new PagingQuery { Page = 1, PageSize = 20 };
            var sortOrder = SortOrder.Parse(string.Empty);
            var queryResult = forecastQueryService.Query("JPN", sortOrder, pagingQuery);
            Assert.IsTrue(queryResult.Count >= 1);
        }

        [TestMethod]
        public void Query_都市コードで取得できる()
        {
            var forecastQueryService = new ForecastQueryService(TestEnvironment.DBSettings);
            var pagingQuery = new PagingQuery { Page = 1, PageSize = 20 };
            var sortOrder = SortOrder.Parse(string.Empty);
            var queryResult = forecastQueryService.Query("NYC", sortOrder, pagingQuery);
            Assert.IsTrue(queryResult.Count >= 1);
        }

        [TestMethod]
        public void Query_サマリーで取得できる()
        {
            var forecastQueryService = new ForecastQueryService(TestEnvironment.DBSettings);
            var pagingQuery = new PagingQuery { Page = 1, PageSize = 20 };
            var sortOrder = SortOrder.Parse(string.Empty);
            var queryResult = forecastQueryService.Query("Cool", sortOrder, pagingQuery);
            Assert.IsTrue(queryResult.Count >= 1);
        }

        [TestMethod]
        public void Query_並び順の昇順で取得できる()
        {
            var forecastQueryService = new ForecastQueryService(TestEnvironment.DBSettings);
            var pagingQuery = new PagingQuery { Page = 1, PageSize = 20 };
            var sortOrder = SortOrder.Parse("CreateTime");
            var queryResult = forecastQueryService.Query(string.Empty, sortOrder, pagingQuery);
            Assert.IsTrue(queryResult[0].CreateTime < queryResult[queryResult.Count - 1].CreateTime);
        }

        [TestMethod]
        public void Query_並び順の降順で取得できる()
        {
            var forecastQueryService = new ForecastQueryService(TestEnvironment.DBSettings);
            var pagingQuery = new PagingQuery { Page = 1, PageSize = 20 };
            var sortOrder = SortOrder.Parse("-CreateTime");
            var queryResult = forecastQueryService.Query(string.Empty, sortOrder, pagingQuery);
            Assert.IsTrue(queryResult[0].CreateTime > queryResult[queryResult.Count - 1].CreateTime);
        }

        [TestMethod]
        public void Query_ホワイトリストに存在しない並び順は無視される()
        {
            var forecastQueryService = new ForecastQueryService(TestEnvironment.DBSettings);
            var pagingQuery = new PagingQuery { Page = 1, PageSize = 20 };
            var sortOrder = SortOrder.Parse("HOGE");
            var queryResult = forecastQueryService.Query(string.Empty, sortOrder, pagingQuery);
        }
    }
}
