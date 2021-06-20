using Microsoft.AspNetCore.DataProtection.Repositories;
using System.Collections.Generic;
using System.Xml.Linq;
using WaterTrans.Boilerplate.CrossCuttingConcerns.Abstractions.OS;
using WaterTrans.Boilerplate.Domain.Abstractions;
using WaterTrans.Boilerplate.Persistence.SqlEntities;

namespace WaterTrans.Boilerplate.Persistence.Repositories
{
    public class DataProtectionRepository : Repository, IXmlRepository
    {
        private readonly SqlRepository<DataProtectionSqlEntity> _sqlRepository;
        private readonly IDateTimeProvider _dateTimeProvider;

        public DataProtectionRepository(IDBSettings dbSettings, IDateTimeProvider dateTimeProvider)
            : base(dbSettings)
        {
            _sqlRepository = new SqlRepository<DataProtectionSqlEntity>(dbSettings);
            _dateTimeProvider = dateTimeProvider;
        }

        public IReadOnlyCollection<XElement> GetAllElements()
        {
            var result = new List<XElement>();
            foreach (var item in _sqlRepository.GetAll())
            {
                result.Add(XElement.Parse(item.Element));
            }
            return result.AsReadOnly();
        }

        public void StoreElement(XElement element, string friendlyName)
        {
            var now = _dateTimeProvider.Now;
            var entity = new DataProtectionSqlEntity
            {
                DataProtectionId = friendlyName,
                Element = element.ToString(),
                CreateTime = now,
                UpdateTime = now,
            };
            _sqlRepository.Create(entity);
        }
    }
}
