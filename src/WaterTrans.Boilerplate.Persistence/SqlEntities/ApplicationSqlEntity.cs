using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WaterTrans.Boilerplate.Persistence.SqlEntities
{
    [Table("Application")]
    internal class ApplicationSqlEntity : SqlEntity
    {
        [Key]
        public Guid ApplicationId { get; set; }
        public string Name { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string Description { get; set; }
        public string Roles { get; set; }
        public string Scopes { get; set; }
        public string GrantTypes { get; set; }
        public string RedirectUris { get; set; }
        public string PostLogoutRedirectUris { get; set; }
        public string Status { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }

        public static implicit operator ApplicationSqlEntity(Domain.Entities.Application entity)
        {
            if (entity is null) { return null; }
            return MP.Mapper.Map<ApplicationSqlEntity>(entity);
        }

        public static implicit operator Domain.Entities.Application(ApplicationSqlEntity sqlEntity)
        {
            if (sqlEntity is null) { return null; }
            return MP.Mapper.Map<Domain.Entities.Application>(sqlEntity);
        }
    }
}
