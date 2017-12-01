using ProjectBase.DAL.DBContext;
using ProjectBase.DAL.Entities.Project;
using ProjectBase.Logic.DTO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectBase.Logic.Services
{
    public class ProjectService : IService<ProjectDTO>
    {
        private static readonly EFProjectBaseContext Context = new EFProjectBaseContext();

        public List<ProjectDTO> GetAll()
        {
            var entities = Context.Projects.ToList();
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
            var entity = Context.Projects.FirstOrDefault(e => e.Id == Id);
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
            Context.Projects.Add(project);
            Context.SaveChanges();
        }

        public void Edit(ProjectDTO item)
        {
            var project = Context.Projects.FirstOrDefault(e => e.Id == item.Id);

            project.Id = item.Id;
            project.Name = item.Name;
            project.CompanyCustomerId = item.CompanyCustomer.Id;
            project.CompanyPerformerId = item.CompanyPerformer.Id;
            project.ProjectChiefId = item.ProjectChief.Id;
            project.StartDate = item.StartDate;
            project.CloseDate = item.CloseDate;
            project.Priority = item.Priority;
            project.Comment = item.Comment;

            Context.SaveChanges();
        }

        public void Delete(Guid Id)
        {
            var project = Find(Id);
            var entity = Context.Projects.FirstOrDefault(e => e.Id == project.Id);

            Context.Projects.Remove(entity);
            Context.SaveChanges();
        }

        public ProjectDTO CreateProjectDTO(ProjectEntity entity)
        {
            var companyCustomerEntity = Context.Companies.FirstOrDefault(c => c.Id == entity.CompanyCustomerId);
            var companyCustomer = new CompanyDTO
            {
                Id = companyCustomerEntity.Id,
                Name = companyCustomerEntity.Name
            };

            var companyPerformerEntity = Context.Companies.FirstOrDefault(c => c.Id == entity.CompanyPerformerId);
            var companyPerformer = new CompanyDTO
            {
                Id = companyPerformerEntity.Id,
                Name = companyPerformerEntity.Name
            };

            var projectChiefEntity = Context.Employees.FirstOrDefault(c => c.Id == entity.ProjectChiefId);
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
