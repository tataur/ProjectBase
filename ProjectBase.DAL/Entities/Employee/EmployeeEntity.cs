using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBase.DAL.Entities.Employee
{
    public class EmployeeEntity
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string Patronymic { get; set; }
        public string Email { get; set; }
        public EmployeeType Status { get; set; }
    }

    public enum EmployeeType
    {
        Chief, Worker
    }
}
