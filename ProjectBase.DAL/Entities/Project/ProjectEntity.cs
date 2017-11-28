using System;
using System.Collections.Generic;
using ProjectBase.DAL.Entities.Employee;
using ProjectBase.DAL.Entities.Company;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectBase.DAL.Entities.Project
{
    [Table("Project")]
    public class ProjectEntity : CommonEntity
    {
        public string Name { get; set; }
        public virtual CompanyEntity CompanyCustomer { get; set; }
        public virtual CompanyEntity CompanyPerformer { get; set; }
        public virtual EmployeeEntity ProjectChief { get; set; }
        public virtual ICollection<ProjectParticipantEntity> ProjectParticipants { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime CloseDate { get; set; }
        public int Priority { get; set; }
        public string Comment { get; set; }
    }
}
