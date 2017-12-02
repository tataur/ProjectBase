using ProjectBase.DAL.DBContext;
using ProjectBase.DAL.Entities.Project;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ProjectBase.DAL.Repositories
{
    public class ProjectRepository : IRepository<ProjectEntity>
    {
        private EFProjectBaseContext context;

        public ProjectRepository(EFProjectBaseContext context)
        {
            this.context = context;
        }

        public void Create(ProjectEntity item)
        {
            context.Projects.Add(item);
        }

        public void Delete(Guid id)
        {
            ProjectEntity item = context.Projects.Find(id);
            if (item != null)
                context.Projects.Remove(item);
        }

        public void Edit(ProjectEntity item)
        {
            context.Entry(item).State = EntityState.Modified;
        }

        public ProjectEntity Find(Guid id)
        {
            return context.Projects.FirstOrDefault(p => p.Id == id);
        }

        public IEnumerable<ProjectEntity> GetAll()
        {
            return context.Projects;
        }
    }
}
