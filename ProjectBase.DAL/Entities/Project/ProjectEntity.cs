using System;
using System.Collections.Generic;
using ProjectBase.DAL.Entities.Employee;

namespace ProjectBase.DAL.Entities.Project
{
    public class ProjectEntity : CommonEntity
    {
        public string Name { get; set; }
        public string CompanyCustomer { get; set; }
        public string CompanyPerformer { get; set; }
        public virtual EmployeeEntity ProjectChief { get; set; }
        public virtual ICollection<ProjectParticipantEntity> ProjectParticipants { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime CloseDate { get; set; }
        public int Priority { get; set; }
        public string Comment { get; set; }
    }
}
