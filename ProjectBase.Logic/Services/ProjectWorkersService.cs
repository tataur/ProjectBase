using ProjectBase.DAL.DBContext;
using ProjectBase.DAL.Entities.Project;
using ProjectBase.DAL.Repositories;
using ProjectBase.Logic.DTO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectBase.Logic.Services
{
    public class ProjectWorkersService : IService<WorkerDTO>
    {
        IUnitOfWork Database { get; set; }

        public ProjectWorkersService()
        {
            Database = new EFUnitOfWork();
        }

        public ProjectWorkersService(IUnitOfWork unit)
        {
            Database = unit;
        }

        public List<WorkerDTO> GetAll()
        {
            var entities = Database.Workers.GetAll().ToList();
            var workersDTO = new List<WorkerDTO>();

            foreach (var item in entities)
            {
                WorkerDTO worker = CreateWorkerDTO(item);
                workersDTO.Add(worker);
            }
            return workersDTO;
        }

        public WorkerDTO Find(Guid Id)
        {
            var entity = Database.Workers.Find(Id);
            WorkerDTO worker = CreateWorkerDTO(entity);
            return worker;
        }

        private WorkerDTO CreateWorkerDTO(ProjectWorkerEntity entity)
        {
            var employeeEntity = Database.Employees.Find(entity.EmployeeId);
            //var employeeDTO = EService.CreateEmployeeDTO(employeeEntity);

            var projectEntity = Database.Projects.Find(entity.ProjectId);
            //var projectDTO = PService.CreateProjectDTO(projectEntity);

            var worker = new WorkerDTO
            {
                Id = entity.Id,
                //Employee = employeeDTO,
                //Project = projectDTO
            };
            return worker;
        }

        public void Create(WorkerDTO item)
        {
            var employeeEntity = Database.Employees.Find(item.Employee.Id);
            var projectEntity = Database.Projects.Find(item.Project.Id);

            var worker = new ProjectWorkerEntity
            {
                EmployeeId = employeeEntity.Id,
                ProjectId = projectEntity.Id
            };
            worker.FillFieldsOnCreate();
            Database.Workers.Create(worker);
        }

        public void Edit(WorkerDTO item)
        {
            var entity = Database.Workers.Find(item.Id);

            entity.EmployeeId = item.Employee.Id;
            entity.ProjectId = item.Project.Id;

            Database.Save();
        }

        public void Delete(Guid Id)
        {
            var worker = Find(Id);
            var entity = Database.Workers.Find(worker.Id);

            Database.Workers.Delete(entity.Id);
            Database.Save();
        }
    }
}
