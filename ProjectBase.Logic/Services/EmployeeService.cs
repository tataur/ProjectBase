using ProjectBase.DAL.DBContext;
using ProjectBase.DAL.Entities.Employee;
using ProjectBase.Logic.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBase.Logic.Services
{
    public class EmployeeService : IService<EmployeeDTO>
    {
        private static readonly EFProjectBaseContext Context = new EFProjectBaseContext();

        public List<EmployeeDTO> GetAll()
        {
            List<EmployeeEntity> employees = Context.Employees.ToList();
            List<EmployeeDTO> em = new List<EmployeeDTO>();
            foreach (var item in employees)
            {
                var emp = new EmployeeDTO
                {
                    Id = item.Id,
                    FirstName = item.FirstName,
                    SecondName = item.SecondName,
                    Patronymic = item.Patronymic,
                    Email = item.Email,
                    IsChief = item.IsChief
                };
                em.Add(emp);
            }
            return em;
        }

        public EmployeeDTO Find(Guid Id)
        {
            var entity = Context.Employees.FirstOrDefault(e => e.Id == Id);
            var employee = new EmployeeDTO
            {
                Id = entity.Id,
                FirstName = entity.FirstName,
                SecondName = entity.SecondName,
                Patronymic = entity.Patronymic,
                Email = entity.Email,
                IsChief = entity.IsChief
            };
            return employee;
        }

        public void Create(EmployeeDTO item)
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

        public void Edit(EmployeeDTO item)
        {
            var employee = Context.Employees.FirstOrDefault(e => e.Id == item.Id);

            //employee.Id = employeeDTO.Id;
            employee.FirstName = item.FirstName;
            employee.SecondName = item.SecondName;
            employee.Patronymic = item.Patronymic;
            employee.Email = item.Email;
            employee.IsChief = item.IsChief;

            Context.SaveChanges();
        }

        public void Delete(Guid Id)
        {
            var employee = Find(Id);
            var entity = Context.Employees.FirstOrDefault(e => e.Id == employee.Id);
            Context.Employees.Remove(entity);
            Context.SaveChanges();
        }
    }
}
