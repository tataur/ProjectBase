using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBase.DAL.Entities.Company
{
    public class CompanyEntity : CommonEntity
    {
        public string Name { get; set; }
        public CompanyType Type { get; set; }
    }

    public enum CompanyType
    {
        Customer, Performer
    }
}
