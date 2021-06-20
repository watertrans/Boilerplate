using System;
using WaterTrans.Boilerplate.Domain.Constants;

namespace WaterTrans.Boilerplate.Domain.Entities
{
    public class AuthorizationCode : Entity
    {
        public string Code { get; set; }
        public Guid ApplicationId { get; set; }
        public Guid AccountId { get; set; }
        public AuthorizationCodeStatus Status { get; set; }
        public DateTime ExpiryTime { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
        public DateTime ConcurrencyToken { get; set; }

        public bool IsEnabled(DateTime currentTime)
        {
            if (Status != AuthorizationCodeStatus.NORMAL)
            {
                return false;
            }

            if (ExpiryTime < currentTime)
            {
                return false;
            }

            return true;
        }
    }
}
