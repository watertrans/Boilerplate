using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WaterTrans.Boilerplate.Domain.Entities;

namespace WaterTrans.Boilerplate.Persistence.SqlEntities
{
    [Table("Account")]
    internal class AccountSqlEntity : SqlEntity
    {
        [Key]
        public Guid AccountId { get; set; }
        public Guid UserId { get; set; }
        public string LoginId { get; set; }
        public byte[] Password { get; set; }
        public byte[] Salt { get; set; }
        public int Iterations { get; set; }
        public string Roles { get; set; }
        public string Status { get; set; }
        public DateTime? LastLoginTime { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }

        public static implicit operator AccountSqlEntity(Account entity)
        {
            if (entity is null) { return null; }
            return MP.Mapper.Map<AccountSqlEntity>(entity);
        }

        public static implicit operator Account(AccountSqlEntity sqlEntity)
        {
            if (sqlEntity is null) { return null; }
            return MP.Mapper.Map<Account>(sqlEntity);
        }
    }
}
