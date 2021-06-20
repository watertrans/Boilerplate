using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WaterTrans.Boilerplate.Domain.Entities;

namespace WaterTrans.Boilerplate.Persistence.SqlEntities
{
    [Table("RefreshToken")]
    internal class RefreshTokenSqlEntity : SqlEntity
    {
        [Key]
        public string Token { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid ApplicationId { get; set; }
        public string PrincipalType { get; set; }
        public Guid PrincipalId { get; set; }
        public string Scopes { get; set; }
        public string Status { get; set; }
        public DateTime ExpiryTime { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }

        public static implicit operator RefreshTokenSqlEntity(RefreshToken entity)
        {
            if (entity is null) { return null; }
            return MP.Mapper.Map<RefreshTokenSqlEntity>(entity);
        }

        public static implicit operator RefreshToken(RefreshTokenSqlEntity sqlEntity)
        {
            if (sqlEntity is null) { return null; }
            return MP.Mapper.Map<RefreshToken>(sqlEntity);
        }
    }
}
