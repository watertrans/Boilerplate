﻿using System;
using WaterTrans.Boilerplate.Domain.Abstractions;
using WaterTrans.Boilerplate.Domain.Abstractions.Repositories;
using WaterTrans.Boilerplate.Domain.Entities;
using WaterTrans.Boilerplate.Persistence.SqlEntities;

namespace WaterTrans.Boilerplate.Persistence.Repositories
{
    public class AccessTokenRepository : Repository, IAccessTokenRepository
    {
        private readonly SqlRepository<AccessTokenSqlEntity> _sqlRepository;

        public AccessTokenRepository(IDBSettings dbSettings)
            : base(dbSettings)
        {
            _sqlRepository = new SqlRepository<AccessTokenSqlEntity>(dbSettings);
        }

        public void Create(AccessToken entity)
        {
            AccessTokenSqlEntity sqlEntity = entity;
            _sqlRepository.Create(sqlEntity);
        }

        public bool Delete(string tokenId)
        {
            return _sqlRepository.Delete(new AccessTokenSqlEntity { TokenId = tokenId });
        }

        public AccessToken GetById(string tokenId)
        {
            AccessToken result = _sqlRepository.GetById(new AccessTokenSqlEntity { TokenId = tokenId });
            return result;
        }

        public bool Update(AccessToken entity)
        {
            AccessTokenSqlEntity sqlEntity = entity;
            return _sqlRepository.Update(sqlEntity);
        }
    }
}
