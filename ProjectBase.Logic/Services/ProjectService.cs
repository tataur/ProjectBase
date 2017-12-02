using ProjectBase.DAL.DBContext;
using ProjectBase.DAL.Entities.Project;
using ProjectBase.DAL.Repositories;
using ProjectBase.Logic.DTO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectBase.Logic.Services
{
    public class ProjectService : IService<ProjectDTO>
    {
        IUnitOfWork Database { get; set; }

        public ProjectService()
        {
            Database = new EFUnitOfWork();
        }

        public ProjectService(IUnitOfWork unit)
        {
            Database = unit;
        }

        public List<CompanyDTO> GetCompanies()
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

        public List<EmployeeDTO> GetEmployees()
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

        public List<WorkerDTO> GetWorkers()
        {
            var entities = Database.Workers.GetAll().ToList();
            var workersDTO = new List<WorkerDTO>();

            foreach (var item in entities)
            {
                EmployeeService employeeService = new EmployeeService();

                var employee = Database.Employees.Find(item.EmployeeId);
                var employeeDTO = employeeService.CreateEmployeeDTO(employee);

                var project = Database.Projects.Find(item.ProjectId);
                var ProjectDTO = CreateProjectDTO(project);

                
                var worker = new WorkerDTO
                {
                    Id = item.Id,
                    Employee = employeeDTO,
                    Project = ProjectDTO
                };
                workersDTO.Add(worker);
            }
            return workersDTO;
        }

        public List<ProjectDTO> GetAll()
        {
            var entities = Database.Projects.GetAll().ToList();
            var projectsDTO = new List<ProjectDTO>();
            foreach (var item in entities)
            {
                var project = CreateProjectDTO(item);
                projectsDTO.Add(project);
            }
            return projectsDTO;
        }

        public ProjectDTO Find(Guid Id)
        {
            var entity = Database.Projects.Find(Id);
            var project = CreateProjectDTO(entity);
            return project;
        }


        public void Create(ProjectDTO item)
        {
            var project = new ProjectEntity
            {
                Id = item.Id,
                Name = item.Name,
                CompanyCustomerId = item.CompanyCustomer.Id,
                CompanyPerformerId = item.CompanyPerformer.Id,
                ProjectChiefId = item.ProjectChief.Id,
                StartDate = item.StartDate,
                CloseDate = item.CloseDate,
                Priority = item.Priority,
                Comment = item.Comment
            };
            project.FillFieldsOnCreate();
            Database.Projects.Create(project);
            Database.Save();
        }

        public void Edit(ProjectDTO item)
        {
            var project = Database.Projects.Find(item.Id);

            project.Id = item.Id;
            project.Name = item.Name;
            project.CompanyCustomerId = item.CompanyCustomer.Id;
            project.CompanyPerformerId = item.CompanyPerformer.Id;
            project.ProjectChiefId = item.ProjectChief.Id;
            project.StartDate = item.StartDate;
            project.CloseDate = item.CloseDate;
            project.Priority = item.Priority;
            project.Comment = item.Comment;

            Database.Save();
        }

        public void Delete(Guid Id)
        {
            var project = Find(Id);
            var entity = Database.Projects.Find(project.Id);

            Database.Projects.Delete(entity.Id);
            Database.Save();
        }

        public ProjectDTO CreateProjectDTO(ProjectEntity entity)
        {
            var companyCustomerEntity = Database.Companies.Find(entity.CompanyCustomerId);
            var companyCustomer = new CompanyDTO
            {
                Id = companyCustomerEntity.Id,
                Name = companyCustomerEntity.Name
            };

            var companyPerformerEntity = Database.Companies.Find(entity.CompanyPerformerId);
            var companyPerformer = new CompanyDTO
            {
                Id = companyPerformerEntity.Id,
                Name = companyPerformerEntity.Name
            };

            var projectChiefEntity = Database.Employees.Find(entity.ProjectChiefId);
            var projectChief = new EmployeeDTO
            {
                Id = projectChiefEntity.Id,
                FirstName = projectChiefEntity.FirstName,
                SecondName = projectChiefEntity.SecondName,
                Patronymic = projectChiefEntity.Patronymic,
                Email = projectChiefEntity.Email,
                IsChief = projectChiefEntity.IsChief
            };

            var project = new ProjectDTO
            {
                Id = entity.Id,
                Name = entity.Name,
                CompanyCustomer = companyCustomer,
                CompanyPerformer = companyPerformer,
                ProjectChief = projectChief,
                StartDate = entity.StartDate,
                CloseDate = entity.CloseDate,
                Priority = entity.Priority,
                Comment = entity.Comment
            };
            return project;
        }
    }
}
