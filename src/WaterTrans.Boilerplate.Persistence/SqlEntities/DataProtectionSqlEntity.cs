using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WaterTrans.Boilerplate.Persistence.SqlEntities
{
    [Table("DataProtection")]
    public class DataProtectionSqlEntity : SqlEntity
    {
        [Key]
        public string DataProtectionId { get; set; }
        public string Element { get; set; }
        public DateTimeOffset CreateTime { get; set; }
        public DateTimeOffset UpdateTime { get; set; }
    }
}
