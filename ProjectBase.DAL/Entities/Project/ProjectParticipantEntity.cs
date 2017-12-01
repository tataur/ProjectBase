using ProjectBase.DAL.Entities.Employee;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectBase.DAL.Entities.Project
{
    [Table("ProjectParticipant")]
    public class ProjectParticipantEntity : CommonEntity
    {
        public Guid EmployeeId { get; set; }
        public Guid ProjectId { get; set; }
    }
}
