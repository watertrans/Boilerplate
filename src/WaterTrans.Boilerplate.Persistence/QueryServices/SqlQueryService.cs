using System;
using System.Data;
using System.Data.Common;
using WaterTrans.Boilerplate.Domain.Abstractions;
using WaterTrans.Boilerplate.Domain.Abstractions.QueryServices;

namespace WaterTrans.Boilerplate.Persistence.QueryServices
{
    public abstract class SqlQueryService : QueryService, ISqlQueryService
    {
        private readonly string _sqlConnectionString;
        private readonly string _replicaSqlConnectionString;

        protected IDbConnection Connection { get; }
        protected DbProviderFactory Factory { get; }

        public SqlQueryService(IDBSettings dbSettings)
            : base(dbSettings)
        {
            Factory = DBSettings.SqlProviderFactory;
            Connection = Factory.CreateConnection();
            Connection.ConnectionString = DBSettings.SqlConnectionString;
            _sqlConnectionString = DBSettings.SqlConnectionString;
            _replicaSqlConnectionString = DBSettings.ReplicaSqlConnectionString;
        }

        public void SwitchReplica()
        {
            if (Connection.State != ConnectionState.Closed)
            {
                throw new InvalidOperationException("The connection was not closed.");
            }

            Connection.ConnectionString = _replicaSqlConnectionString;
        }

        public void SwitchOriginal()
        {
            if (Connection.State != ConnectionState.Closed)
            {
                throw new InvalidOperationException("The connection was not closed.");
            }

            Connection.ConnectionString = _sqlConnectionString;
        }
    }
}
