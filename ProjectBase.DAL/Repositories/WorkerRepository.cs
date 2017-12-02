using ProjectBase.DAL.DBContext;
using ProjectBase.DAL.Entities.Project;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ProjectBase.DAL.Repositories
{
    public class WorkerRepository : IRepository<ProjectWorkerEntity>
    {
        private EFProjectBaseContext context;

        public WorkerRepository(EFProjectBaseContext context)
        {
            this.context = context;
        }

        public void Create(ProjectWorkerEntity item)
        {
            context.Workers.Add(item);
        }

        public void Delete(Guid id)
        {
            ProjectWorkerEntity item = context.Workers.Find(id);
            if (item != null)
                context.Workers.Remove(item);
        }

        public void Edit(ProjectWorkerEntity item)
        {
            context.Entry(item).State = EntityState.Modified;
        }

        public ProjectWorkerEntity Find(Guid id)
        {
            return context.Workers.FirstOrDefault(w => w.Id == id);
        }

        public IEnumerable<ProjectWorkerEntity> GetAll()
        {
            return context.Workers;
        }
    }
}
