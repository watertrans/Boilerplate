namespace WaterTrans.Boilerplate.Domain.Abstractions.QueryServices
{
    public interface ISqlQueryService
    {
        void SwitchReplica();
        void SwitchOriginal();
    }
}
