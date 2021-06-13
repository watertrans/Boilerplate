using Dapper.FastCrud;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using WaterTrans.Boilerplate.Domain.Abstractions;
using WaterTrans.Boilerplate.Persistence.Exceptions;
using WaterTrans.Boilerplate.Persistence.SqlEntities;

namespace WaterTrans.Boilerplate.Persistence.Repositories
{
    public class SqlRepository<TSqlEntity> : Repository, ISqlRepository<TSqlEntity>
        where TSqlEntity : SqlEntity
    {
        protected IDbConnection Connection { get; }

        protected DbProviderFactory Factory { get; }

        public SqlRepository(IDBSettings dbSettings)
            : base(dbSettings)
        {
            Factory = DBSettings.SqlProviderFactory;
            Connection = Factory.CreateConnection();
            Connection.ConnectionString = DBSettings.SqlConnectionString;
        }

        public virtual void Create(TSqlEntity sqlEntity)
        {
            try
            {
                Connection.Insert<TSqlEntity>(sqlEntity, statement => statement
                    .WithTimeout(TimeSpan.FromSeconds(DBSettings.CommandTimeout)));
            }
            //catch (SqlException ex) when (ex.Number == 2601 | ex.Number == 2627)
            //{
            //    throw new DuplicateKeyException("The primary key is duplicated.", ex);
            //}
            catch (MySqlException ex) when (ex.Number == 1022 | ex.Number == 1062)
            {
                throw new DuplicateKeyException("The primary key is duplicated.", ex);
            }
        }

        public async virtual Task CreateAsync(TSqlEntity sqlEntity)
        {
            try
            {
                await Connection.InsertAsync<TSqlEntity>(sqlEntity, statement => statement
                    .WithTimeout(TimeSpan.FromSeconds(DBSettings.CommandTimeout)));
            }
            //catch (SqlException ex) when (ex.Number == 2601 | ex.Number == 2627)
            //{
            //    throw new DuplicateKeyException("The primary key is duplicated.", ex);
            //}
            catch (MySqlException ex) when (ex.Number == 1022 | ex.Number == 1062)
            {
                throw new DuplicateKeyException("The primary key is duplicated.", ex);
            }
        }

        public virtual IEnumerable<TSqlEntity> GetAll()
        {
            return Connection.Find<TSqlEntity>();
        }

        public async virtual Task<IEnumerable<TSqlEntity>> GetAllAsync()
        {
            return await Connection.FindAsync<TSqlEntity>();
        }

        public virtual TSqlEntity GetById(TSqlEntity sqlEntity)
        {
            return Connection.Get<TSqlEntity>(sqlEntity, statement => statement
                .WithTimeout(TimeSpan.FromSeconds(DBSettings.CommandTimeout)));
        }

        public async virtual Task<TSqlEntity> GetByIdAsync(TSqlEntity sqlEntity)
        {
            return await Connection.GetAsync<TSqlEntity>(sqlEntity, statement => statement
                .WithTimeout(TimeSpan.FromSeconds(DBSettings.CommandTimeout)));
        }

        public virtual bool Update(TSqlEntity sqlEntity)
        {
            var mappings = OrmConfiguration.GetDefaultEntityMapping<TSqlEntity>().Clone();
            mappings.SetProperty(p => p.ConcurrencyToken, setup => setup.SetPrimaryKey());
            return Connection.Update<TSqlEntity>(sqlEntity, statement => statement
                .WithEntityMappingOverride(mappings)
                .WithTimeout(TimeSpan.FromSeconds(DBSettings.CommandTimeout)));
        }

        public async virtual Task<bool> UpdateAsync(TSqlEntity sqlEntity)
        {
            var mappings = OrmConfiguration.GetDefaultEntityMapping<TSqlEntity>().Clone();
            mappings.SetProperty(p => p.ConcurrencyToken, setup => setup.SetPrimaryKey());
            return await Connection.UpdateAsync<TSqlEntity>(sqlEntity, statement => statement
                .WithEntityMappingOverride(mappings)
                .WithTimeout(TimeSpan.FromSeconds(DBSettings.CommandTimeout)));
        }

        public virtual bool Delete(TSqlEntity sqlEntity)
        {
            return Connection.Delete<TSqlEntity>(sqlEntity, statement => statement
                .WithTimeout(TimeSpan.FromSeconds(DBSettings.CommandTimeout)));
        }

        public async virtual Task<bool> DeleteAsync(TSqlEntity sqlEntity)
        {
            return await Connection.DeleteAsync<TSqlEntity>(sqlEntity, statement => statement
                .WithTimeout(TimeSpan.FromSeconds(DBSettings.CommandTimeout)));
        }
    }
}
