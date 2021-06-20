using System;
using WaterTrans.Boilerplate.Domain.Abstractions;
using WaterTrans.Boilerplate.Domain.Abstractions.Repositories;
using WaterTrans.Boilerplate.Domain.Entities;
using WaterTrans.Boilerplate.Persistence.SqlEntities;
using WaterTrans.Boilerplate.Persistence.TableDataGateways;

namespace WaterTrans.Boilerplate.Persistence.Repositories
{
    public class ApplicationRepository : Repository, IApplicationRepository
    {
        private readonly SqlTableDataGateway<ApplicationSqlEntity> _sqlTableDataGateway;

        public ApplicationRepository(IDBSettings dbSettings)
            : base(dbSettings)
        {
            _sqlTableDataGateway = new SqlTableDataGateway<ApplicationSqlEntity>(dbSettings);
        }

        public void Create(Domain.Entities.Application entity)
        {
            ApplicationSqlEntity sqlEntity = entity;
            _sqlTableDataGateway.Create(sqlEntity);
        }

        public bool Delete(Guid applicationId)
        {
            return _sqlTableDataGateway.Delete(new ApplicationSqlEntity { ApplicationId = applicationId });
        }

        public Domain.Entities.Application GetById(Guid applicationId)
        {
            Domain.Entities.Application result = _sqlTableDataGateway.GetById(new ApplicationSqlEntity { ApplicationId = applicationId });
            return result;
        }

        public bool Update(Domain.Entities.Application entity)
        {
            ApplicationSqlEntity sqlEntity = entity;
            return _sqlTableDataGateway.Update(sqlEntity);
        }
    }
}
