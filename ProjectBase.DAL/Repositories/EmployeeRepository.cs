using ProjectBase.DAL.DBContext;
using ProjectBase.DAL.Entities.Employee;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ProjectBase.DAL.Repositories
{
    public class EmployeeRepository : IRepository<EmployeeEntity>
    {
        private EFProjectBaseContext context;

        public EmployeeRepository(EFProjectBaseContext context)
        {
            this.context = context;
        }

        public IEnumerable<EmployeeEntity> GetAll()
        {
            return context.Employees;
        }

        public void Create(EmployeeEntity item)
        {
            context.Employees.Add(item);
        }

        public void Edit(EmployeeEntity item)
        {
            context.Entry(item).State = EntityState.Modified;
        }

        public EmployeeEntity Find(Guid id)
        {
            return context.Employees.FirstOrDefault(e=>e.Id == id);
        }

        public void Delete(Guid id)
        {
            EmployeeEntity item = context.Employees.Find(id);
            if (item != null)
                context.Employees.Remove(item);
        }
    }
}
