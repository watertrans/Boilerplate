using WaterTrans.Boilerplate.Domain.Entities;

namespace WaterTrans.Boilerplate.Domain.Abstractions.Repositories
{
    public interface IAccessTokenRepository
    {
        void Create(AccessToken entity);
        AccessToken GetById(string tokenId);
        bool Update(AccessToken entity);
        bool Delete(string tokenId);
    }
}
