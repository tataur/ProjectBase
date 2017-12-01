using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectBase.DAL.Entities.Project
{
    [Table("Project")]
    public class ProjectEntity : CommonEntity
    {
        public string Name { get; set; }
        public Guid CompanyCustomerId { get; set; }
        public Guid CompanyPerformerId { get; set; }
        public Guid ProjectChiefId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime CloseDate { get; set; }
        public int Priority { get; set; }
        public string Comment { get; set; }
    }
}
