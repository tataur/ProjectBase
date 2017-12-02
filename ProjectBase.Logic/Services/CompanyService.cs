using ProjectBase.DAL.DBContext;
using ProjectBase.DAL.Repositories;
using ProjectBase.Logic.DTO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectBase.Logic.Services
{
    public class CompanyService : IService<CompanyDTO>
    {
        IUnitOfWork Database { get; set; }

        public CompanyService()
        {
            Database = new EFUnitOfWork();
        }

        public CompanyService(IUnitOfWork unit)
        {
            Database = unit;
        }

        public List<CompanyDTO> GetAll()
        {
            var entities = Database.Companies.GetAll().ToList();
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
