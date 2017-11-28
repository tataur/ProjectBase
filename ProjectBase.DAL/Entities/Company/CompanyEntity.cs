using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectBase.DAL.Entities.Company
{
    [Table("Company")]
    public class CompanyEntity : CommonEntity
    {
        public string Name { get; set; }
        public bool IsCustomer { get; set; }
    }
}
