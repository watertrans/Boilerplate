using WaterTrans.Boilerplate.Domain.Abstractions;

namespace WaterTrans.Boilerplate.Application.Settings
{
    public class AppSettings : IAppSettings
    {
        public int AccessTokenExpiresIn { get; set; }
        public int AuthorizationCodeExpiresIn { get; set; }
        public int RefreshTokenExpiresIn { get; set; }
    }
}
