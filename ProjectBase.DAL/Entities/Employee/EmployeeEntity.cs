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
    }
}
