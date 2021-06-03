using WaterTrans.Boilerplate.Domain.Entities;

namespace WaterTrans.Boilerplate.Domain.Abstractions.Repositories
{
    public interface IAuthorizationCodeRepository
    {
        void Create(AuthorizationCode entity);
        AuthorizationCode GetById(string code);
        bool Update(AuthorizationCode entity);
        bool Delete(string code);
    }
}
