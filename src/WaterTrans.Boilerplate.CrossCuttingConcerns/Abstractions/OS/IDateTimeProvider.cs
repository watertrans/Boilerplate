using System;

namespace WaterTrans.Boilerplate.CrossCuttingConcerns.Abstractions.OS
{
    public interface IDateTimeProvider
    {
        DateTime Now { get; }
    }
}
