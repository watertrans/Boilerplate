using System;
using System.Collections.Generic;
using WaterTrans.Boilerplate.Domain.Constants;

namespace WaterTrans.Boilerplate.Domain.Entities
{
    public class RefreshToken : Entity
    {
        public string Token { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid ApplicationId { get; set; }
        public string PrincipalType { get; set; }
        public Guid PrincipalId { get; set; }
        public List<string> Roles { get; set; }
        public List<string> Scopes { get; set; }
        public RefreshTokenStatus Status { get; set; }
        public DateTime ExpiryTime { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
        public DateTime ConcurrencyToken { get; set; }

        public bool IsEnabled(DateTime currentTime)
        {
            if (Status != RefreshTokenStatus.NORMAL)
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
