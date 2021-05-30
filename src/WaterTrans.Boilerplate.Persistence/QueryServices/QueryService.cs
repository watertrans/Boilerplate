using WaterTrans.Boilerplate.Domain.Abstractions;

namespace WaterTrans.Boilerplate.Persistence.QueryServices
{
    public abstract class QueryService
    {
        protected IDBSettings DBSettings { get; }

        public QueryService(IDBSettings dbSettings)
        {
            DBSettings = dbSettings;
        }
    }
}
