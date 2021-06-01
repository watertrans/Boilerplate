using WaterTrans.Boilerplate.Domain.Abstractions;
using WaterTrans.Boilerplate.Domain.Abstractions.Repositories;
using WaterTrans.Boilerplate.Domain.Entities;
using WaterTrans.Boilerplate.Persistence.SqlEntities;

namespace WaterTrans.Boilerplate.Persistence.Repositories
{
    public class AuthorizationCodeRepository : Repository, IAuthorizationCodeRepository
    {
        private readonly SqlRepository<AuthorizationCodeSqlEntity> _sqlRepository;

        public AuthorizationCodeRepository(IDBSettings dbSettings)
            : base(dbSettings)
        {
            _sqlRepository = new SqlRepository<AuthorizationCodeSqlEntity>(dbSettings);
        }

        public void Create(AuthorizationCode entity)
        {
            AuthorizationCodeSqlEntity sqlEntity = entity;
            _sqlRepository.Create(sqlEntity);
        }

        public bool Delete(string codeId)
        {
            return _sqlRepository.Delete(new AuthorizationCodeSqlEntity { CodeId = codeId });
        }

        public AuthorizationCode GetById(string codeId)
        {
            AuthorizationCode result = _sqlRepository.GetById(new AuthorizationCodeSqlEntity { CodeId = codeId });
            return result;
        }

        public bool Update(AuthorizationCode entity)
        {
            AuthorizationCodeSqlEntity sqlEntity = entity;
            return _sqlRepository.Update(sqlEntity);
        }
    }
}
