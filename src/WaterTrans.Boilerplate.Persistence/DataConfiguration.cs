using Dapper.FastCrud;

namespace WaterTrans.Boilerplate.Persistence
{
    public static class DataConfiguration
    {
        public static void Initialize()
        {
            OrmConfiguration.DefaultDialect = SqlDialect.MySql;
        }
    }
}
