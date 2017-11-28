using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectBase.Web.Models.Employee
{
    public class EmployeeModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string Patronymic { get; set; }
        public string Email { get; set; }
        public bool IsChief { get; set; }
    }
}