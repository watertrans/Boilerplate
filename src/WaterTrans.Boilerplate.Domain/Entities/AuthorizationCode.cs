using System;
using WaterTrans.Boilerplate.Domain.Constants;

namespace WaterTrans.Boilerplate.Domain.Entities
{
    public class AuthorizationCode : Entity
    {
        public string CodeId { get; set; }
        public Guid ApplicationId { get; set; }
        public Guid AccountId { get; set; }
        public AuthorizationCodeStatus Status { get; set; }
        public DateTimeOffset ExpiryTime { get; set; }
        public DateTimeOffset CreateTime { get; set; }
        public DateTimeOffset UpdateTime { get; set; }
    }
}
