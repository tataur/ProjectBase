using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

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
                return "Человек без имени";
            }
            return FirstName + " " + SecondName + " " + Patronymic;
        }

        public static explicit operator EmployeeEntity(List<EmployeeEntity> v)
        {
            throw new NotImplementedException();
        }
    }
}
