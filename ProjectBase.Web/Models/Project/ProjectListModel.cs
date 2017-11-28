using ProjectBase.DAL.Entities.Project;
using System.Collections.Generic;

namespace ProjectBase.Web.Models.Project
{
    public class ProjectListModel
    {
        public List<ProjectEntity> Projects { get; set; }

        public string SearchString { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string SortField { get; set; }
        public string SortDir { get; set; }
    }
}