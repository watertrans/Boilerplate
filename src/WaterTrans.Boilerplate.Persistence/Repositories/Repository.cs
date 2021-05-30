using WaterTrans.Boilerplate.Domain.Abstractions;

namespace WaterTrans.Boilerplate.Persistence.Repositories
{
    public abstract class Repository
    {
        protected IDBSettings DBSettings { get; }
        public Repository(IDBSettings dbSettings)
        {
            DBSettings = dbSettings;
        }
    }
}
