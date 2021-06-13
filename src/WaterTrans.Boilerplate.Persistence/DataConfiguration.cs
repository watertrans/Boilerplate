using Dapper;
using Dapper.FastCrud;
using WaterTrans.Boilerplate.Persistence.TypeHandlers;

namespace WaterTrans.Boilerplate.Persistence
{
    public static class DataConfiguration
    {
        public static void Initialize()
        {
            OrmConfiguration.DefaultDialect = SqlDialect.MySql;
            SqlMapper.AddTypeHandler(new DateTimeHandler());
        }
    }
}
