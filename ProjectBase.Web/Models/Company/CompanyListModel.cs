using ProjectBase.DAL.Entities.Company;
using System.Collections.Generic;

namespace ProjectBase.Web.Models.Company
{
    public class CompanyListModel
    {
        public List<CompanyEntity> Companies { get; set; }
    }
}