namespace WaterTrans.Boilerplate.Domain.Abstractions
{
    public interface IAppSettings
    {
        int AccessTokenExpiresIn { get; }
        int AuthorizationCodeExpiresIn { get; }
        int RefreshTokenExpiresIn { get; }
    }
}
