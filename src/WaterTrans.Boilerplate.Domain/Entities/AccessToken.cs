using System;
using System.Collections.Generic;
using WaterTrans.Boilerplate.Domain.Constants;

namespace WaterTrans.Boilerplate.Domain.Entities
{
    public class AccessToken : Entity
    {
        public string TokenId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid ApplicationId { get; set; }
        public string PrincipalType { get; set; }
        public Guid PrincipalId { get; set; }
        public List<string> Roles { get; set; }
        public List<string> Scopes { get; set; }
        public AccessTokenStatus Status { get; set; }
        public DateTimeOffset ExpiryTime { get; set; }
        public DateTimeOffset CreateTime { get; set; }
        public DateTimeOffset UpdateTime { get; set; }
    }
}
