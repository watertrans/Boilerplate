using WaterTrans.Boilerplate.Domain.Entities;

namespace WaterTrans.Boilerplate.Domain.Abstractions.Repositories
{
    public interface IAuthorizationCodeRepository
    {
        void Create(AuthorizationCode entity);
        AuthorizationCode GetById(string codeId);
        bool Update(AuthorizationCode entity);
        bool Delete(string codeId);
    }
}
