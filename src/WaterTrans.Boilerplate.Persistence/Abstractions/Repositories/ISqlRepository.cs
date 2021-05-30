﻿using System.Threading.Tasks;
using WaterTrans.Boilerplate.Persistence.SqlEntities;

namespace WaterTrans.Boilerplate.Persistence.Repositories
{
    public interface ISqlRepository<TSqlEntity>
        where TSqlEntity : SqlEntity
    {
        void Create(TSqlEntity entity);
        Task CreateAsync(TSqlEntity entity);
        TSqlEntity Read(TSqlEntity entity);
        Task<TSqlEntity> ReadAsync(TSqlEntity entity);
        bool Update(TSqlEntity entity);
        Task<bool> UpdateAsync(TSqlEntity entity);
        bool Delete(TSqlEntity entity);
        Task<bool> DeleteAsync(TSqlEntity entity);
    }
}
