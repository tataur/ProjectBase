using ProjectBase.DAL.Entities.Employee;
using ProjectBase.DAL.Repositories;
using ProjectBase.Logic.DTO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectBase.Logic.Services
{
    public class EmployeeService : IService<EmployeeDTO>
    {
        IUnitOfWork Database { get; set; }

        public EmployeeService()
        {
            Database = new EFUnitOfWork();
        }

        public EmployeeService(IUnitOfWork unit)
        {
            Database = unit;
        }

        public List<EmployeeDTO> GetAll()
        {
            var entities = Database.Employees.GetAll().ToList();
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
            var entity = Database.Employees.Find(Id);
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
            Database.Employees.Create(employee);
            Database.Save();
        }

        public void Edit(EmployeeDTO item)
        {
            var entity = Database.Employees.Find(item.Id);

            entity.FirstName = item.FirstName;
            entity.SecondName = item.SecondName;
            entity.Patronymic = item.Patronymic;
            entity.Email = item.Email;
            entity.IsChief = item.IsChief;

            Database.Save();
        }

        public void Delete(Guid Id)
        {
            var employee = Find(Id);
            var entity = Database.Employees.Find(Id);

            var workers = Database.Workers.GetAll().Where(p => p.EmployeeId == employee.Id);
            foreach (var item in workers)
            {
                Database.Workers.Delete(item.Id);
            }

            Database.Employees.Delete(entity.Id);
            Database.Save();
        }
    }
}
