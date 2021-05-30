using System;
using System.Collections.Generic;
using WaterTrans.Boilerplate.Domain.Constants;

namespace WaterTrans.Boilerplate.Domain.Entities
{
    public class Application : Entity
    {
        public Guid ApplicationId { get; set; }
        public string Name { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string Description { get; set; }
        public List<string> Roles { get; set; }
        public List<string> Scopes { get; set; }
        public List<string> GrantTypes { get; set; }
        public List<string> RedirectUris { get; set; }
        public List<string> PostLogoutRedirectUris { get; set; }
        public ApplicationStatus Status { get; set; }
        public DateTimeOffset CreateTime { get; set; }
        public DateTimeOffset UpdateTime { get; set; }
    }
}
