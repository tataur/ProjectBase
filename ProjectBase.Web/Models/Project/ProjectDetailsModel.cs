using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectBase.Web.Models.Project
{
    public class ProjectDetailsModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string CompanyCustomer { get; set; }
        public string CompanyPerformer { get; set; }
        public string ProjectChief { get; set; }
        //public string ParticipantIdsJson { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime CloseDate { get; set; }
        public int Priority { get; set; }
        public string Comment { get; set; }
    }
}