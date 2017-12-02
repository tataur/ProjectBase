using ProjectBase.DAL.DBContext;
using ProjectBase.DAL.Entities.Company;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBase.DAL.Repositories
{
    public class CompanyRepository : IRepository<CompanyEntity>
    {
        private EFProjectBaseContext context;

        public CompanyRepository(EFProjectBaseContext context)
        {
            this.context = context;
        }

        public void Create(CompanyEntity item)
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Edit(CompanyEntity item)
        {
            throw new NotImplementedException();
        }

        public CompanyEntity Find(Guid id)
        {
            return context.Companies.FirstOrDefault(e => e.Id == id);
        }

        public IEnumerable<CompanyEntity> GetAll()
        {
            return context.Companies;
        }
    }
}
