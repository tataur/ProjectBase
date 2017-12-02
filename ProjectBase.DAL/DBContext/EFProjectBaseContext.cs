using System.Data.Entity;
using ProjectBase.DAL.Entities.Employee;
using ProjectBase.DAL.Entities.Company;
using ProjectBase.DAL.Entities.Project;

namespace ProjectBase.DAL.DBContext
{
    public class EFProjectBaseContext : DbContext
    {
        public DbSet<EmployeeEntity> Employees { get; set; }
        public DbSet<CompanyEntity> Companies { get; set; }
        public DbSet<ProjectWorkerEntity> Workers { get; set; }
        public DbSet<ProjectEntity> Projects { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<EFProjectBaseContext>(null);
            base.OnModelCreating(modelBuilder);
        }
    }
}
