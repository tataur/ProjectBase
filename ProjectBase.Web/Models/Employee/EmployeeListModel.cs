using ProjectBase.DAL.Entities.Employee;
using System.Collections.Generic;

namespace ProjectBase.Web.Models.Employee
{
    public class EmployeeListModel
    {
        public List<EmployeeEntity> Employees { get; set; }   
    }
}