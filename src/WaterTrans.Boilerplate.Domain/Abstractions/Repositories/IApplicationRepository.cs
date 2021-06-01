using System;
using WaterTrans.Boilerplate.Domain.Entities;

namespace WaterTrans.Boilerplate.Domain.Abstractions.Repositories
{
    public interface IApplicationRepository
    {
        void Create(Application entity);
        Application GetById(Guid applicationId);
        bool Update(Application entity);
        bool Delete(Guid applicationId);
    }
}
