using System;
using ProjectBase.DAL.DBContext;
using ProjectBase.DAL.Entities.Employee;
using ProjectBase.DAL.Entities.Project;
using ProjectBase.DAL.Entities.Company;

namespace ProjectBase.DAL.Repositories
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private EFProjectBaseContext db;
        private EmployeeRepository employeeRepository;
        private ProjectRepository projectRepository;
        private WorkerRepository workerRepository;
        private CompanyRepository companyRepository;

        public EFUnitOfWork()
        {
            db = new EFProjectBaseContext();
        }
        public IRepository<EmployeeEntity> Employees
        {
            get
            {
                if (employeeRepository == null)
                    employeeRepository = new EmployeeRepository(db);
                return employeeRepository;
            }
        }

        public IRepository<ProjectEntity> Projects
        {
            get
            {
                if (projectRepository == null)
                    projectRepository = new ProjectRepository(db);
                return projectRepository;
            }
        }

        public IRepository<ProjectWorkerEntity> Workers
        {
            get
            {
                if (workerRepository == null)
                    workerRepository = new WorkerRepository(db);
                return workerRepository;
            }
        }

        public IRepository<CompanyEntity> Companies
        {
            get
            {
                if (companyRepository == null)
                    companyRepository = new CompanyRepository(db);
                return companyRepository;
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
