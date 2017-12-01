using ProjectBase.DAL.DBContext;
using ProjectBase.Logic.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBase.Logic.Services
{
    public class CompanyService : IService<CompanyDTO>
    {
        private static readonly EFProjectBaseContext Context = new EFProjectBaseContext();

        public List<CompanyDTO> GetAll()
        {
            var entities = Context.Companies.ToList();
            var companiesDTO = new List<CompanyDTO>();

            foreach (var item in entities)
            {
                var company = new CompanyDTO
                {
                    Id = item.Id,
                    Name = item.Name
                };
                companiesDTO.Add(company);
            }
            return companiesDTO;
        }

        public void Create(CompanyDTO item)
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid Id)
        {
            throw new NotImplementedException();
        }

        public void Edit(CompanyDTO item)
        {
            throw new NotImplementedException();
        }

        public CompanyDTO Find(Guid Id)
        {
            throw new NotImplementedException();
        }
    }
}
