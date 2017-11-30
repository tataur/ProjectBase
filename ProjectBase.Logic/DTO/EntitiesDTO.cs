using ProjectBase.DAL.DBContext;
using ProjectBase.DAL.Entities.Company;
using ProjectBase.DAL.Entities.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBase.Logic.DTO
{
    public class EntitiesDTO
    {
        private static readonly EFProjectBaseContext Context = new EFProjectBaseContext();

        public EmployeeDTO Employee { get; set; }
        public List<EmployeeDTO> Employees { get; set; }

        public CompanyDTO Company { get; set; }
        public List<CompanyDTO> Companies { get; set; }

        public ProjectDTO Project { get; set; }
        public List<ProjectDTO> Projects { get; set; }

        public ParticipantDTO Participant { get; set; }
        public List<ParticipantDTO> Participants { get; set; }
    }

    public class EmployeeDTO
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string Patronymic { get; set; }
        public string Email { get; set; }
        public bool IsChief { get; set; }
    }

    public class CompanyDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }

    public class ProjectDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public CompanyDTO CompanyCustomer { get; set; }
        public CompanyDTO CompanyPerformer { get; set; }
        public EmployeeDTO ProjectChief { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime CloseDate { get; set; }
        public int Priority { get; set; }
        public string Comment { get; set; }
    }

    public class ParticipantDTO
    {
        public EmployeeDTO Employee { get; set; }
        public ProjectDTO Project { get; set; }
    }
}
