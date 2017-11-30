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
            var companyCustomer = Context.Companies.FirstOrDefault(c => c.Id == item.CompanyCustomer.Id);
            var companyPerformer = Context.Companies.FirstOrDefault(c => c.Id == item.CompanyPerformer.Id);
            var projectChief = Context.Employees.FirstOrDefault(c => c.Id == item.ProjectChief.Id);

            var project = new ProjectEntity
            {
                Id = item.Id,
                Name = item.Name,
                CompanyCustomer = companyCustomer,
                CompanyPerformer = companyPerformer,
                ProjectChief = projectChief,
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

            var companyCustomer = Context.Companies.FirstOrDefault(c => c.Id == item.CompanyCustomer.Id);
            var companyPerformer = Context.Companies.FirstOrDefault(c => c.Id == item.CompanyPerformer.Id);
            var projectChief = Context.Employees.FirstOrDefault(c => c.Id == item.ProjectChief.Id);

            project.Id = item.Id;
            project.Name = item.Name;
            project.CompanyCustomer = companyCustomer;
            project.CompanyPerformer = companyPerformer;
            project.ProjectChief = projectChief;
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
            var companyCustomer = new CompanyDTO
            {
                Id = entity.CompanyCustomer.Id,
                Name = entity.CompanyCustomer.Name
            };

            var companyPerformer = new CompanyDTO
            {
                Id = entity.CompanyPerformer.Id,
                Name = entity.CompanyPerformer.Name
            };

            var projectChief = new EmployeeDTO
            {
                Id = entity.ProjectChief.Id,
                FirstName = entity.ProjectChief.FirstName,
                SecondName = entity.ProjectChief.SecondName,
                Patronymic = entity.ProjectChief.Patronymic,
                Email = entity.ProjectChief.Email,
                IsChief = entity.ProjectChief.IsChief
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
