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
        public void Query_���݂��Ȃ�ID�Ō��ʂ��[����()
        {
            var forecastQueryService = new ForecastQueryService(TestEnvironment.DBSettings);
            var pagingQuery = new PagingQuery { Page = 1, PageSize = 20 };
            var sortOrder = SortOrder.Parse(string.Empty);
            var queryResult = forecastQueryService.Query("00000000-0000-0000-0000-000000000000", sortOrder, pagingQuery);
            Assert.IsTrue(queryResult.Count == 0);
        }

        [TestMethod]
        public void Query_�v���C�}���[�L�[�Ŏ擾�ł���()
        {
            var forecastQueryService = new ForecastQueryService(TestEnvironment.DBSettings);
            var pagingQuery = new PagingQuery { Page = 1, PageSize = 20 };
            var sortOrder = SortOrder.Parse(string.Empty);
            var queryResult = forecastQueryService.Query("00000000-0000-0000-0000-000000000001", sortOrder, pagingQuery);
            Assert.IsTrue(queryResult.Count == 1);
        }

        [TestMethod]
        public void Query_���݂��Ȃ��y�[�W���w�肵�Č��ʂ��[����()
        {
            var forecastQueryService = new ForecastQueryService(TestEnvironment.DBSettings);
            var pagingQuery = new PagingQuery { Page = 999, PageSize = 20 };
            var sortOrder = SortOrder.Parse(string.Empty);
            var queryResult = forecastQueryService.Query("00000000-0000-0000-0000-000000000001", sortOrder, pagingQuery);
            Assert.IsTrue(queryResult.Count == 0);
        }

        [TestMethod]
        public void Query_���R�[�h�Ŏ擾�ł���()
        {
            var forecastQueryService = new ForecastQueryService(TestEnvironment.DBSettings);
            var pagingQuery = new PagingQuery { Page = 1, PageSize = 20 };
            var sortOrder = SortOrder.Parse(string.Empty);
            var queryResult = forecastQueryService.Query("JPN", sortOrder, pagingQuery);
            Assert.IsTrue(queryResult.Count >= 1);
        }

        [TestMethod]
        public void Query_�s�s�R�[�h�Ŏ擾�ł���()
        {
            var forecastQueryService = new ForecastQueryService(TestEnvironment.DBSettings);
            var pagingQuery = new PagingQuery { Page = 1, PageSize = 20 };
            var sortOrder = SortOrder.Parse(string.Empty);
            var queryResult = forecastQueryService.Query("NYC", sortOrder, pagingQuery);
            Assert.IsTrue(queryResult.Count >= 1);
        }

        [TestMethod]
        public void Query_�T�}���[�Ŏ擾�ł���()
        {
            var forecastQueryService = new ForecastQueryService(TestEnvironment.DBSettings);
            var pagingQuery = new PagingQuery { Page = 1, PageSize = 20 };
            var sortOrder = SortOrder.Parse(string.Empty);
            var queryResult = forecastQueryService.Query("Cool", sortOrder, pagingQuery);
            Assert.IsTrue(queryResult.Count >= 1);
        }

        [TestMethod]
        public void Query_���я��̏����Ŏ擾�ł���()
        {
            var forecastQueryService = new ForecastQueryService(TestEnvironment.DBSettings);
            var pagingQuery = new PagingQuery { Page = 1, PageSize = 20 };
            var sortOrder = SortOrder.Parse("CreateTime");
            var queryResult = forecastQueryService.Query(string.Empty, sortOrder, pagingQuery);
            Assert.IsTrue(queryResult[0].CreateTime < queryResult[queryResult.Count - 1].CreateTime);
        }

        [TestMethod]
        public void Query_���я��̍~���Ŏ擾�ł���()
        {
            var forecastQueryService = new ForecastQueryService(TestEnvironment.DBSettings);
            var pagingQuery = new PagingQuery { Page = 1, PageSize = 20 };
            var sortOrder = SortOrder.Parse("-CreateTime");
            var queryResult = forecastQueryService.Query(string.Empty, sortOrder, pagingQuery);
            Assert.IsTrue(queryResult[0].CreateTime > queryResult[queryResult.Count - 1].CreateTime);
        }

        [TestMethod]
        public void Query_�z���C�g���X�g�ɑ��݂��Ȃ����я��͖��������()
        {
            var forecastQueryService = new ForecastQueryService(TestEnvironment.DBSettings);
            var pagingQuery = new PagingQuery { Page = 1, PageSize = 20 };
            var sortOrder = SortOrder.Parse("HOGE");
            var queryResult = forecastQueryService.Query(string.Empty, sortOrder, pagingQuery);
        }
    }
}
