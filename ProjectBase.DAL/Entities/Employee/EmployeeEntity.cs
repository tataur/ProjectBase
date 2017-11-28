using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBase.DAL.Entities.Employee
{
    [Table("Employee")]
    public class EmployeeEntity : CommonEntity
    {
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string Patronymic { get; set; }
        public string Email { get; set; }
        public bool IsChief { get; set; }

        public string GetFullName()
        {
            if (string.IsNullOrWhiteSpace(FirstName) && string.IsNullOrWhiteSpace(SecondName) && string.IsNullOrWhiteSpace(Patronymic))
            {
                return "ID:" + Id;
            }
            var separator = !string.IsNullOrWhiteSpace(FirstName) && !string.IsNullOrWhiteSpace(SecondName) && !string.IsNullOrWhiteSpace(Patronymic) ? " " : "";
            return FirstName + separator + SecondName + separator + Patronymic;
        }
    }
}
