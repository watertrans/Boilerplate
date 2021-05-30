using System;

namespace WaterTrans.Boilerplate.Domain.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException(string entityName, string entityId)
        {
            EntityName = entityName;
            EntityId = entityId;
        }

        public string EntityName { get; }
        public string EntityId { get; }
        public override string Message
        {
            get
            {
                return $"EntityName: {EntityName} EntityId: {EntityId}";
            }
        }
    }
}
