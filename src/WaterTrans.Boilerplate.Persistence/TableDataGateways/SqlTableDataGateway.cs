using Dapper.FastCrud;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using WaterTrans.Boilerplate.Domain.Abstractions;
using WaterTrans.Boilerplate.Persistence.Exceptions;
using WaterTrans.Boilerplate.Persistence.SqlEntities;

namespace WaterTrans.Boilerplate.Persistence.TableDataGateways
{
    internal class SqlTableDataGateway<TSqlEntity> : ISqlTableDataGateway<TSqlEntity>
        where TSqlEntity : SqlEntity
    {
        private readonly IDbConnection _connection;
        private readonly DbProviderFactory _factory;
        private readonly IDBSettings _dbSettings;

        public SqlTableDataGateway(IDBSettings dbSettings)
        {
            _factory = dbSettings.SqlProviderFactory;
            _connection = _factory.CreateConnection();
            _connection.ConnectionString = dbSettings.SqlConnectionString;
            _dbSettings = dbSettings;
        }

        public void Create(TSqlEntity sqlEntity)
        {
            try
            {
                _connection.Insert<TSqlEntity>(sqlEntity, statement => statement
                    .WithTimeout(TimeSpan.FromSeconds(_dbSettings.CommandTimeout)));
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

        public async Task CreateAsync(TSqlEntity sqlEntity)
        {
            try
            {
                await _connection.InsertAsync<TSqlEntity>(sqlEntity, statement => statement
                    .WithTimeout(TimeSpan.FromSeconds(_dbSettings.CommandTimeout)));
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

        public IEnumerable<TSqlEntity> GetAll()
        {
            return _connection.Find<TSqlEntity>();
        }

        public async Task<IEnumerable<TSqlEntity>> GetAllAsync()
        {
            return await _connection.FindAsync<TSqlEntity>();
        }

        public TSqlEntity GetById(TSqlEntity sqlEntity)
        {
            return _connection.Get<TSqlEntity>(sqlEntity, statement => statement
                .WithTimeout(TimeSpan.FromSeconds(_dbSettings.CommandTimeout)));
        }

        public async Task<TSqlEntity> GetByIdAsync(TSqlEntity sqlEntity)
        {
            return await _connection.GetAsync<TSqlEntity>(sqlEntity, statement => statement
                .WithTimeout(TimeSpan.FromSeconds(_dbSettings.CommandTimeout)));
        }

        public bool Update(TSqlEntity sqlEntity)
        {
            var mappings = OrmConfiguration.GetDefaultEntityMapping<TSqlEntity>().Clone();
            mappings.SetProperty(p => p.ConcurrencyToken, setup => setup.SetPrimaryKey());
            return _connection.Update<TSqlEntity>(sqlEntity, statement => statement
                .WithEntityMappingOverride(mappings)
                .WithTimeout(TimeSpan.FromSeconds(_dbSettings.CommandTimeout)));
        }

        public async Task<bool> UpdateAsync(TSqlEntity sqlEntity)
        {
            var mappings = OrmConfiguration.GetDefaultEntityMapping<TSqlEntity>().Clone();
            mappings.SetProperty(p => p.ConcurrencyToken, setup => setup.SetPrimaryKey());
            return await _connection.UpdateAsync<TSqlEntity>(sqlEntity, statement => statement
                .WithEntityMappingOverride(mappings)
                .WithTimeout(TimeSpan.FromSeconds(_dbSettings.CommandTimeout)));
        }

        public bool Delete(TSqlEntity sqlEntity)
        {
            return _connection.Delete<TSqlEntity>(sqlEntity, statement => statement
                .WithTimeout(TimeSpan.FromSeconds(_dbSettings.CommandTimeout)));
        }

        public async Task<bool> DeleteAsync(TSqlEntity sqlEntity)
        {
            return await _connection.DeleteAsync<TSqlEntity>(sqlEntity, statement => statement
                .WithTimeout(TimeSpan.FromSeconds(_dbSettings.CommandTimeout)));
        }
    }
}
