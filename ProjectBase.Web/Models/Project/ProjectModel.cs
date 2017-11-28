using System;

namespace ProjectBase.Web.Models.Project
{
    public class ProjectModel
    {
        public string Name { get; set; }
        public Guid CompanyCustomerId { get; set; }
        public Guid CompanyPerformerId { get; set; }
        public Guid ProjectChiefId { get; set; }
        public string ParticipantIdsJson { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime CloseDate { get; set; }
        public int Priority { get; set; }
        public string Comment { get; set; }
    }
}