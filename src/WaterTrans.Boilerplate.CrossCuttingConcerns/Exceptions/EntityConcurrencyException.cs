using System;

namespace WaterTrans.Boilerplate.CrossCuttingConcerns.Exceptions
{
    public class EntityConcurrencyException : Exception
    {
        public EntityConcurrencyException(string entityName, string entityId)
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
