using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WaterTrans.Boilerplate.Domain.Entities;

namespace WaterTrans.Boilerplate.Persistence.SqlEntities
{
    [Table("AuthorizationCode")]
    public class AuthorizationCodeSqlEntity : SqlEntity
    {
        [Key]
        public string CodeId { get; set; }
        public Guid ApplicationId { get; set; }
        public Guid AccountId { get; set; }
        public string Status { get; set; }
        public DateTimeOffset ExpiryTime { get; set; }
        public DateTimeOffset CreateTime { get; set; }
        public DateTimeOffset UpdateTime { get; set; }

        public static implicit operator AuthorizationCodeSqlEntity(AuthorizationCode entity)
        {
            if (entity is null) { return null; }
            return MP.Mapper.Map<AuthorizationCodeSqlEntity>(entity);
        }

        public static implicit operator AuthorizationCode(AuthorizationCodeSqlEntity sqlEntity)
        {
            if (sqlEntity is null) { return null; }
            return MP.Mapper.Map<AuthorizationCode>(sqlEntity);
        }
    }
}
