using System.Collections.Generic;
using WaterTrans.Boilerplate.Domain.Entities;

namespace WaterTrans.Boilerplate.Domain.Abstractions.QueryServices
{
    public interface IForecastQueryService : ISqlQueryService
    {
        IList<Forecast> Query(string query, SortOrder sort, PagingQuery paging);
    }
}
