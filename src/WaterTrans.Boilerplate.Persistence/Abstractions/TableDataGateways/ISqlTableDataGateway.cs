using System.Collections.Generic;
using System.Threading.Tasks;
using WaterTrans.Boilerplate.Persistence.SqlEntities;

namespace WaterTrans.Boilerplate.Persistence.TableDataGateways
{
    internal interface ISqlTableDataGateway<TSqlEntity>
        where TSqlEntity : SqlEntity
    {
        void Create(TSqlEntity entity);
        Task CreateAsync(TSqlEntity entity);
        TSqlEntity GetById(TSqlEntity entity);
        Task<TSqlEntity> GetByIdAsync(TSqlEntity entity);
        IEnumerable<TSqlEntity> GetAll();
        Task<IEnumerable<TSqlEntity>> GetAllAsync();
        bool Update(TSqlEntity entity);
        Task<bool> UpdateAsync(TSqlEntity entity);
        bool Delete(TSqlEntity entity);
        Task<bool> DeleteAsync(TSqlEntity entity);
        IEnumerable<TSqlEntity> ExecuteQuery(string sql, object param);
        Task<IEnumerable<TSqlEntity>> ExecuteQueryAsync(string sql, object param);
    }
}
