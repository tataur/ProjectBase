using ProjectBase.DAL.DBContext;
using ProjectBase.DAL.Entities.Project;
using ProjectBase.Logic.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBase.Logic.Services
{
    public class ProjectService : IService<ProjectDTO>
    {
        private static readonly EFProjectBaseContext Context = new EFProjectBaseContext();

        public List<ProjectDTO> GetAll()
        {
            List<ProjectEntity> projects = Context.Projects.ToList();
            List<ProjectDTO> pr = new List<ProjectDTO>();
            foreach (var item in projects)
            {
                var pro = CreateProjectDTO(item);
                pr.Add(pro);
            }
            return pr;
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
                CompanyCustomer = item.CompanyCustomer,
                CompanyPerformer = item.CompanyPerformer,
                ProjectChief = item.ProjectChief,
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

            project.Name = item.Name;
            project.CompanyCustomer = item.CompanyCustomer;
            project.CompanyPerformer = item.CompanyPerformer;
            project.ProjectChief = item.ProjectChief;
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
            var project = new ProjectDTO
            {
                Id = entity.Id,
                Name = entity.Name,
                CompanyCustomer = entity.CompanyCustomer,
                CompanyPerformer = entity.CompanyPerformer,
                ProjectChief = entity.ProjectChief,
                StartDate = entity.StartDate,
                CloseDate = entity.CloseDate,
                Priority = entity.Priority,
                Comment = entity.Comment
            };
            return project;
        }
    }
}
