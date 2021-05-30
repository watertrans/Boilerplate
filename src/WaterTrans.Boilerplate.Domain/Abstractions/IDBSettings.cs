using System.Data.Common;

namespace WaterTrans.Boilerplate.Domain.Abstractions
{
    public interface IDBSettings
    {
        DbProviderFactory SqlProviderFactory { get; }
        string StorageConnectionString { get; }
        string SqlConnectionString { get; }
        string ReplicaSqlConnectionString { get; }
        int CommandTimeout { get; }
    }
}
