using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WaterTrans.Boilerplate.Domain.Entities;

namespace WaterTrans.Boilerplate.Persistence.SqlEntities
{
    [Table("Forecast")]
    public class ForecastSqlEntity : SqlEntity
    {
        [Key]
        public Guid ForecastId { get; set; }
        public string ForecastCode { get; set; }
        public string CountryCode { get; set; }
        public string CityCode { get; set; }
        public DateTime Date { get; set; }
        public int Temperature { get; set; }
        public string Summary { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }

        public static implicit operator ForecastSqlEntity(Forecast entity)
        {
            if (entity is null) { return null; }
            return MP.Mapper.Map<ForecastSqlEntity>(entity);
        }

        public static implicit operator Forecast(ForecastSqlEntity sqlEntity)
        {
            if (sqlEntity is null) { return null; }
            return MP.Mapper.Map<Forecast>(sqlEntity);
        }
    }
}
