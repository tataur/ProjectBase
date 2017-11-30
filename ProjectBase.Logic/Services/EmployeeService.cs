using ProjectBase.DAL.DBContext;
using ProjectBase.DAL.Entities.Employee;
using ProjectBase.Logic.DTO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectBase.Logic.Services
{
    public class EmployeeService : IService<EmployeeDTO>
    {
        private static readonly EFProjectBaseContext Context = new EFProjectBaseContext();

        public List<EmployeeDTO> GetAll()
        {
            var entities = Context.Employees.ToList();
            var employeesDTO = new List<EmployeeDTO>();

            foreach (var item in entities)
            {
                var employee = new EmployeeDTO
                {
                    Id = item.Id,
                    FirstName = item.FirstName,
                    SecondName = item.SecondName,
                    Patronymic = item.Patronymic,
                    Email = item.Email,
                    IsChief = item.IsChief
                };
                employeesDTO.Add(employee);
            }
            return employeesDTO;
        }

        public EmployeeDTO Find(Guid Id)
        {
            var entity = Context.Employees.FirstOrDefault(e => e.Id == Id);
            EmployeeDTO employee = CreateEmployeeDTO(entity);
            return employee;
        }

        public EmployeeDTO CreateEmployeeDTO(EmployeeEntity entity)
        {
            return new EmployeeDTO
            {
                Id = entity.Id,
                FirstName = entity.FirstName,
                SecondName = entity.SecondName,
                Patronymic = entity.Patronymic,
                Email = entity.Email,
                IsChief = entity.IsChief
            };
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
            var entity = Context.Employees.FirstOrDefault(e => e.Id == item.Id);

            entity.FirstName = item.FirstName;
            entity.SecondName = item.SecondName;
            entity.Patronymic = item.Patronymic;
            entity.Email = item.Email;
            entity.IsChief = item.IsChief;

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
