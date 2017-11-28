using ProjectBase.DAL.DBContext;
using ProjectBase.DAL.Entities.Employee;
using ProjectBase.Logic.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectBase.Logic.Concrete
{
    public class EmployeeRepo : ICommonRepository<EmployeeEntity>
    {
        private readonly EFProjectBaseContext Context = new EFProjectBaseContext();

        public void Create(EmployeeEntity item)
        {
            var employee = new EmployeeEntity
            {
                FirstName = item.FirstName,
                SecondName = item.SecondName,
                Patronymic = item.Patronymic,
                Email = item.Email,
                IsChief = item.IsChief
            };
            employee.FillFieldsOnCreate();
            Context.Employees.Add(employee);
            Context.SaveChanges();
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public EmployeeEntity Get(Guid id)
        {
            return Context.Employees.FirstOrDefault(e => e.Id == id);
        }

        public List<EmployeeEntity> GetAll()
        {
            return Context.Employees.ToList();
        }

        public void Update(EmployeeEntity item)
        {
            throw new NotImplementedException();
        }
    }
}
