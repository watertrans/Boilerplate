using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace WaterTrans.Boilerplate.Persistence.SqlEntities
{
    public class SqlEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime ConcurrencyToken { get; set; }
    }
}
