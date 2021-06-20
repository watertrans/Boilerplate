using WaterTrans.Boilerplate.Domain.Entities;

namespace WaterTrans.Boilerplate.Domain.Abstractions.Repositories
{
    public interface IRefreshTokenRepository
    {
        void Create(RefreshToken entity);
        RefreshToken GetById(string token);
        bool Update(RefreshToken entity);
        bool Delete(string token);
    }
}
