using NLog;
using ProjectBase.Logic.DTO;
using ProjectBase.Logic.Services;
using ProjectBase.Web.Models;
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
        private readonly ProjectService PService = new ProjectService();

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
            var q = PService.GetAll();

            // search
            //if (!string.IsNullOrWhiteSpace(search))
            //{
            //    q = from e in q
            //        where e.Name.Contains(search)
            //            || e.CompanyCustomer.Name.Contains(search)
            //            || e.CompanyPerformer.Name.Contains(search)
            //            || e.ProjectChief.FirstName.Contains(search)
            //            || e.ProjectChief.SecondName.Contains(search)
            //            || e.ProjectChief.Patronymic.Contains(search)
            //            || e.Priority.ToString().Contains(search)
            //        select e;
            //}

            return View("List", new ContextModel
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
        public ActionResult Create(ContextModel model)
        {
            if (ModelState.IsValid)
            {
                PService.Create(model.Project);
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
            logger.Info("Details() called");
            logger.Info("Id: " + Id);

            var project = PService.Find(Id);
            ContextModel model = CreateModel(project);
            return View(model);
        }

        [HttpGet]
        public ActionResult Edit(Guid Id)
        {
            logger.Info("Edit() called Get");
            logger.Info("Id: " + Id);

            var project = PService.Find(Id);
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

        private static ContextModel CreateModel(ProjectDTO project)
        {
            var projectModel = new ContextModel
            {
                Project = new ProjectDTO
                {
                    Id = project.Id,
                    Name = project.Name,
                    CompanyCustomer = project.CompanyCustomer,
                    CompanyPerformer = project.CompanyPerformer,
                    ProjectChief = project.ProjectChief,
                    StartDate = project.StartDate,
                    CloseDate = project.CloseDate,
                    Priority = project.Priority,
                    Comment = project.Comment
                }
            };
            return projectModel;
        }

        public JsonResult DeleteParticipant(Guid employeeId, Guid projectId)
        {
            logger.Info("DeleteParticipant() called");
            logger.Debug("employeeId = " + employeeId);
            logger.Debug("projectId = " + projectId);

            var employee = Context.Employees.FirstOrDefault(e => e.Id == employeeId);
            var project = Context.Projects.FirstOrDefault(p => p.Id == projectId);

            var participant = Context.Participants.Where(p => p.Project.Id == projectId).FirstOrDefault(e => e.Employee.Id == employeeId);
            logger.Debug("participant.Employee.Id = " + participant.Employee.Id);
            logger.Debug("participant.Project.Id = " + participant.Project.Id);
            logger.Debug("participant.Id = " + participant.Id);

            Context.Participants.Remove(participant);
            Context.SaveChanges();
            logger.Info("participant removed");

            return Json(null, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AddParticipant(Guid employeeId, Guid projectId)
        {
            logger.Info("AddParticipant() called");
            logger.Debug("id = " + employeeId);
            logger.Debug("projectId = " + projectId);

            var employee = Context.Employees.FirstOrDefault(e => e.Id == employeeId);
            var project = Context.Projects.FirstOrDefault(p => p.Id == projectId);

            var participant = new ProjectParticipantEntity
            {
                Employee = employee,
                Project = project
            };
            participant.FillFieldsOnCreate();
            Context.Participants.Add(participant);
            Context.SaveChanges();
            logger.Info("participant added");

            return Json(null, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetParticipants(Guid projectId)
        {
            logger.Info("GetParticipants() called");
            logger.Debug("projectId = " + projectId);

            var participants = Context.Participants.Where(p => p.Project.Id == projectId).ToList();

            if (participants != null)
            {
                logger.Info(participants.Count());

                List<ParticipantsJsonModel> participantsList = participants
                    .Select(
                        entity =>
                        new ParticipantsJsonModel
                        {
                            id = entity.Employee.Id.ToString(),
                            name = entity.Employee.GetFullName(),
                        })
                    .ToList();
                return Json(participantsList.ToArray(), JsonRequestBehavior.AllowGet);
            }
            else
            {
                logger.Info("нет участников");

                return Json(null, JsonRequestBehavior.AllowGet);
            }

        }
    }
}