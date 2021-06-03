using Dapper;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WaterTrans.Boilerplate.Domain;
using WaterTrans.Boilerplate.Domain.Abstractions;
using WaterTrans.Boilerplate.Domain.Abstractions.QueryServices;
using WaterTrans.Boilerplate.Domain.Constants;
using WaterTrans.Boilerplate.Domain.Entities;
using WaterTrans.Boilerplate.Persistence.SqlEntities;

namespace WaterTrans.Boilerplate.Persistence.QueryServices
{
    public class ForecastQueryService : SqlQueryService, IForecastQueryService
    {
        public ForecastQueryService(IDBSettings dbSettings)
            : base(dbSettings)
        {
        }

        public IList<Forecast> Query(string query, SortOrder sort, PagingQuery paging)
        {
            var sqlWhere = new StringBuilder();

            if (!string.IsNullOrEmpty(query))
            {
                sqlWhere.AppendLine(" AND ( ");
                sqlWhere.AppendLine("     `ForecastId` LIKE @Query OR ");
                sqlWhere.AppendLine("     `ForecastCode` LIKE @Query OR ");
                sqlWhere.AppendLine("     `CountryCode` LIKE @Query OR ");
                sqlWhere.AppendLine("     `CityCode` LIKE @Query OR ");
                sqlWhere.AppendLine("     `Summary` LIKE @Query ");
                sqlWhere.AppendLine(" ) ");
            }

            var param = new
            {
                Query = DataUtil.EscapeLike(query, LikeMatchType.PrefixSearch),
                Page = paging.Page,
                PageSize = paging.PageSize,
                Offset = (paging.Page - 1) * paging.PageSize,
            };

            var sqlCount = new StringBuilder();
            sqlCount.AppendLine(" SELECT COUNT(*) FROM `Forecast` WHERE  1 = 1 ");
            sqlCount.AppendLine(sqlWhere.ToString());

            paging.TotalCount = (long)Connection.ExecuteScalar(sqlCount.ToString(),param, commandTimeout: DBSettings.CommandTimeout);

            var sqlSort = DataUtil.ConvertToOrderBy(sort, "Date", "CountryCode", "CityCode", "CreateTime");
            if (sqlSort == string.Empty)
            {
                sqlSort = " ORDER BY `Date`, `CountryCode`, `CityCode` ";
            }

            var sql = new StringBuilder();

            sql.AppendLine(" SELECT * ");
            sql.AppendLine("   FROM `Forecast` ");
            sql.AppendLine("  WHERE 1 = 1 ");
            sql.AppendLine(sqlWhere.ToString());
            sql.AppendLine(sqlSort.ToString());
            sql.AppendLine("  LIMIT @PageSize OFFSET @Offset ");

            var sqlResult = Connection.Query<ForecastSqlEntity>(sql.ToString(), param, commandTimeout: DBSettings.CommandTimeout);
            return sqlResult.Select<ForecastSqlEntity, Forecast>(x => x).ToList();
        }
    }
}
