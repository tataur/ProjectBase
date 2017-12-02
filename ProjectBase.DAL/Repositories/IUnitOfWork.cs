using System;
using ProjectBase.DAL.Entities.Employee;
using ProjectBase.DAL.Entities.Project;
using ProjectBase.DAL.Entities.Company;

namespace ProjectBase.DAL.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<EmployeeEntity> Employees { get; }
        IRepository<ProjectEntity> Projects { get; }
        IRepository<ProjectWorkerEntity> Workers { get; }
        IRepository<CompanyEntity> Companies { get; }
        void Save();
    }
}
