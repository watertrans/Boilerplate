using WaterTrans.Boilerplate.Domain.Abstractions;

namespace WaterTrans.Boilerplate.Application.Settings
{
    public class EnvSettings : IEnvSettings
    {
        public bool IsDebug { get; set; }
    }
}
