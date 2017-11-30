using ProjectBase.Logic.DTO;
using System.Collections.Generic;

namespace ProjectBase.Web.Models
{
    public class ContextModel
    {
        public EmployeeDTO Employee { get; set; }
        public List<EmployeeDTO> Employees { get; set; }

        public ProjectDTO Project { get; set; }
        public List<ProjectDTO> Projects { get; set; }
    }
}