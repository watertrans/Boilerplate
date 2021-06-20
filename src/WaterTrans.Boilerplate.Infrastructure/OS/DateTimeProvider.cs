using System;
using WaterTrans.Boilerplate.CrossCuttingConcerns.Abstractions.OS;

namespace WaterTrans.Boilerplate.Infrastructure.OS
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime Now => DateTime.UtcNow;
    }
}
