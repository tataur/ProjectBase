using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectBase.DAL.Entities.Employee
{
    [Table("Employee")]
    public class EmployeeEntity : CommonEntity
    {

        [Required(ErrorMessage = "Введите имя")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Введите фамилию")]
        public string SecondName { get; set; }

        [Required(ErrorMessage = "Введите отчество")]
        public string Patronymic { get; set; }

        [Required(ErrorMessage = "Введите email")]
        public string Email { get; set; }

        public bool IsChief { get; set; }
    }
}
