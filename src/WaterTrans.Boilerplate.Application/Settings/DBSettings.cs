using System.Data.Common;
using WaterTrans.Boilerplate.Domain.Abstractions;

namespace WaterTrans.Boilerplate.Application.Settings
{
    public class DBSettings : IDBSettings
    {
        public string StorageConnectionString { get; set; }
        public string SqlConnectionString { get; set; }
        public string ReplicaSqlConnectionString { get; set; }
        public DbProviderFactory SqlProviderFactory { get; set; }
        public int CommandTimeout { get; set; }
    }
}
