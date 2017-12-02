using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectBase.DAL.Entities.Project
{
    [Table("ProjectWorker")]
    public class ProjectWorkerEntity : CommonEntity
    {
        public Guid EmployeeId { get; set; }
        public Guid ProjectId { get; set; }
    }
}
