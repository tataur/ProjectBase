using NLog;
using ProjectBase.DAL.DBContext;
using ProjectBase.DAL.Entities.Project;
using ProjectBase.Web.Models.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectBase.Web.Controllers
{
    public class ProjectController : Controller
    {
        private static Logger logger = LogManager.GetLogger("Projects");
        private readonly EFProjectBaseContext Context = new EFProjectBaseContext();

        // GET: Employee
        public ActionResult Index()
        {
            return List(null, null, null);
        }

        public static object ListParams(ProjectListModel model, int pageNum, string sf = null, string sd = null)
        {
            return new
            {
                page = pageNum,
                search = model.SearchString,
                fromDate = model.FromDate,
                toDate = model.ToDate,
                sf = sf ?? model.SortField,
                sd = sd ?? model.SortDir,
            };
        }

        public ActionResult List(string search, string fromDate, string toDate, string sf = "cdate", string sd = "desc", bool showDeleted = false)
        {
            IQueryable<ProjectEntity> q = Context.Projects;

            // search
            if (!string.IsNullOrWhiteSpace(search))
            {
                q = from e in q
                    where e.Name.Contains(search)
                        || e.CompanyCustomer.Name.Contains(search)
                        || e.CompanyPerformer.Name.Contains(search)
                        || e.ProjectChief.FirstName.Contains(search)
                        || e.ProjectChief.SecondName.Contains(search)
                        || e.ProjectChief.Patronymic.Contains(search)
                        || e.Priority.ToString().Contains(search)
                    select e;
            }

            return View("List", new ProjectListModel
            {
                Projects = q.ToList()
            });
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View(new ProjectModel());
        }

        [HttpPost]
        public ActionResult Create(ProjectModel model)
        {
            if (ModelState.IsValid)
            {
                var companyCustomer = Context.Companies.FirstOrDefault(c => c.Id == model.CompanyCustomerId);
                var companyPerformer = Context.Companies.FirstOrDefault(c => c.Id == model.CompanyPerformerId);
                var projectChief = Context.Employees.FirstOrDefault(c => c.Id == model.ProjectChiefId);

                //participants

                var project = new ProjectEntity
                {
                    Id = model.Id,
                    Name = model.Name,
                    CompanyCustomer = companyCustomer,
                    CompanyPerformer = companyPerformer,
                    ProjectChief = projectChief,
                    StartDate = model.StartDate,
                    CloseDate = model.CloseDate,
                    Priority = model.Priority,
                    Comment = model.Comment
                };
                project.FillFieldsOnCreate();
                Context.Projects.Add(project);
                Context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        //private static string BuildParticipantIdsJson(List<ProjectParticipantEntity> participants)
        //{
        //    var ids = new ParticipantsJson();

        //    foreach (ProjectParticipantEntity participant in participants)
        //    {
        //        ids.Participants.Add(new ParticipantJson()
        //        {
        //            EmployeeId = participant.Employee.Id.ToString(),
        //        });
        //    }
        //    var idsJson = JsonConvert.SerializeObject(ids);
        //    return idsJson;
        //}

        public ActionResult Details(Guid Id)
        {
            var project = Context.Projects.FirstOrDefault(p => p.Id == Id);
            ProjectDetailsModel model = new ProjectDetailsModel
            {
                Id = project.Id,
                Name = project.Name,
                CompanyCustomer = project.CompanyCustomer.Name,
                CompanyPerformer = project.CompanyPerformer.Name,
                ProjectChief = project.ProjectChief.GetFullName(),
                StartDate = project.StartDate,
                CloseDate = project.CloseDate,
                Priority = project.Priority,
                Comment = project.Comment
            };
            return View(model);
        }

        private static ProjectModel CreateModel(ProjectEntity project)
        {
            var model = new ProjectModel
            {
                Id = project.Id,
                Name = project.Name,
                CompanyCustomerId = project.CompanyCustomer.Id,
                CompanyPerformerId = project.CompanyPerformer.Id,
                ProjectChiefId = project.ProjectChief.Id,
                StartDate = project.StartDate,
                CloseDate = project.CloseDate,
                Priority = project.Priority,
                Comment = project.Comment
            };
            return model;
        }

        [HttpGet]
        public ActionResult Edit(Guid Id)
        {
            logger.Info("Edit() called Get");
            logger.Info("Id: " + Id);

            var project = Context.Projects.FirstOrDefault(p => p.Id == Id);
            var model = CreateModel(project);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(ProjectModel model)
        {
            logger.Info("Edit() called Post");
            logger.Debug("model.Id: " + model.Id);
            logger.Debug("model.Name: " + model.Name);
            logger.Debug("model.CompanyCustomerId: " + model.CompanyCustomerId);
            logger.Debug("model.CompanyPerformerId: " + model.CompanyPerformerId);
            logger.Debug("model.ProjectChiefId: " + model.ProjectChiefId);
            logger.Debug("model.StartDate: " + model.StartDate);
            logger.Debug("model.CloseDate: " + model.CloseDate);
            //logger.Debug("model.ParticipantIdsJson: " + model.ParticipantIdsJson);
            logger.Debug("model.Priority: " + model.Priority);
            logger.Debug("model.Comment: " + model.Comment);

            var project = Context.Projects.FirstOrDefault(p => p.Id == model.Id);
            var companyCustomer = Context.Companies.FirstOrDefault(c => c.Id == model.CompanyCustomerId);
            var companyPerformer = Context.Companies.FirstOrDefault(c => c.Id == model.CompanyPerformerId);
            var projectChief = Context.Employees.FirstOrDefault(c => c.Id == model.ProjectChiefId);

            if (ModelState.IsValid && project != null)
            {
                project.Id = model.Id;
                project.Name = model.Name;
                project.CompanyCustomer = companyCustomer;
                project.CompanyPerformer = companyPerformer;
                project.ProjectChief = projectChief;
                project.StartDate = model.StartDate;
                project.CloseDate = model.CloseDate;
                project.Priority = model.Priority;
                project.Comment = model.Comment;

                Context.SaveChanges();
            }
            return RedirectToAction("Details", new { Id = project.Id });
        }
    }
}