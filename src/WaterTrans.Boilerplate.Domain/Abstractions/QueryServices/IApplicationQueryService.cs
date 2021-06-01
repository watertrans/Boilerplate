using WaterTrans.Boilerplate.Domain.Entities;

namespace WaterTrans.Boilerplate.Domain.Abstractions.QueryServices
{
    public interface IApplicationQueryService : ISqlQueryService
    {
        Application GetByClientId(string clientId);
    }
}
