using ProjectBase.DAL.Entities.Employee;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectBase.DAL.Entities.Project
{
    [Table("ProjectParticipant")]
    public class ProjectParticipantEntity : CommonEntity
    {
        public virtual EmployeeEntity Employee { get; set; }
        public virtual ProjectEntity Project { get; set; }
    }
}
