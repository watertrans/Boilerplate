using System;
using WaterTrans.Boilerplate.Domain.Abstractions;
using WaterTrans.Boilerplate.Domain.Abstractions.Repositories;
using WaterTrans.Boilerplate.Domain.Entities;
using WaterTrans.Boilerplate.Persistence.SqlEntities;

namespace WaterTrans.Boilerplate.Persistence.Repositories
{
    public class ApplicationRepository : Repository, IApplicationRepository
    {
        private readonly SqlRepository<ApplicationSqlEntity> _sqlRepository;

        public ApplicationRepository(IDBSettings dbSettings)
            : base(dbSettings)
        {
            _sqlRepository = new SqlRepository<ApplicationSqlEntity>(dbSettings);
        }

        public void Create(Domain.Entities.Application entity)
        {
            ApplicationSqlEntity sqlEntity = entity;
            _sqlRepository.Create(sqlEntity);
        }

        public bool Delete(Guid applicationId)
        {
            return _sqlRepository.Delete(new ApplicationSqlEntity { ApplicationId = applicationId });
        }

        public Domain.Entities.Application GetById(Guid applicationId)
        {
            Domain.Entities.Application result = _sqlRepository.GetById(new ApplicationSqlEntity { ApplicationId = applicationId });
            return result;
        }

        public bool Update(Domain.Entities.Application entity)
        {
            ApplicationSqlEntity sqlEntity = entity;
            return _sqlRepository.Update(sqlEntity);
        }
    }
}
