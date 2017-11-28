using ProjectBase.DAL.Entities.Project;
using System.Collections.Generic;

namespace ProjectBase.Web.Models.Project
{
    public class ProjectListModel
    {
        public List<ProjectEntity> Projects { get; set; }
    }
}