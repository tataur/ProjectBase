using ProjectBase.DAL.Entities.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBase.DAL.Entities.Project
{
    public class ProjectParticipantEntity : CommonEntity
    {
        public virtual EmployeeEntity Employee { get; set; }
        public virtual ProjectEntity Project { get; set; }
    }
}
