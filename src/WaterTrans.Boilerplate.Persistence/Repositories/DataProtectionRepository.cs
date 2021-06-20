using Microsoft.AspNetCore.DataProtection.Repositories;
using System.Collections.Generic;
using System.Xml.Linq;
using WaterTrans.Boilerplate.CrossCuttingConcerns.Abstractions.OS;
using WaterTrans.Boilerplate.Domain.Abstractions;
using WaterTrans.Boilerplate.Persistence.SqlEntities;
using WaterTrans.Boilerplate.Persistence.TableDataGateways;

namespace WaterTrans.Boilerplate.Persistence.Repositories
{
    public class DataProtectionRepository : Repository, IXmlRepository
    {
        private readonly SqlTableDataGateway<DataProtectionSqlEntity> _sqlTableDataGateway;
        private readonly IDateTimeProvider _dateTimeProvider;

        public DataProtectionRepository(IDBSettings dbSettings, IDateTimeProvider dateTimeProvider)
            : base(dbSettings)
        {
            _sqlTableDataGateway = new SqlTableDataGateway<DataProtectionSqlEntity>(dbSettings);
            _dateTimeProvider = dateTimeProvider;
        }

        public IReadOnlyCollection<XElement> GetAllElements()
        {
            var result = new List<XElement>();
            foreach (var item in _sqlTableDataGateway.GetAll())
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
            _sqlTableDataGateway.Create(entity);
        }
    }
}
