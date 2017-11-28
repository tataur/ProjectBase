using ProjectBase.DAL.Entities.Company;
using ProjectBase.DAL.Entities.Employee;
using ProjectBase.DAL.Entities.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProjectBase.DAL.Abstract
{
    public interface IUnitOfWork : IDisposable
    {
        ICommonRepository<EmployeeEntity> Employees { get; }
        ICommonRepository<CompanyEntity> Companies { get; }
        ICommonRepository<ProjectParticipantEntity> Participants { get; }
        ICommonRepository<ProjectEntity> Projects { get; }
    }
}
