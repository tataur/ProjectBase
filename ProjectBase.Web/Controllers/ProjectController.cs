using NLog;
using ProjectBase.Logic.DTO;
using ProjectBase.Logic.Services;
using ProjectBase.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ProjectBase.Web.Controllers
{
    public class ProjectController : Controller
    {
        private static Logger logger = LogManager.GetLogger("Projects");
        private readonly ProjectService PService = new ProjectService();
        private readonly EmployeeService EService = new EmployeeService();
        private readonly CompanyService CService = new CompanyService();
        private readonly ProjectParticipantsService PartService = new ProjectParticipantsService();

        // GET: Employee
        public ActionResult Index(Guid? customer, Guid? performer, int? priority, int page = 1)
        {
            return List(customer, performer, priority, page);
        }

        public ActionResult List(Guid? customer, Guid? performer, int? priority, int page = 1)
        {
            logger.Info("List() called");
            logger.Info("customer", customer);
            logger.Info("performer", performer);
            logger.Info("priority", priority);

            int pageItems = 10;

            IQueryable<ProjectDTO> q = PService.GetAll().AsQueryable();
            logger.Debug("q.Count(): " + q.Count());

            List<CompanyDTO> customers = CService.GetAll().ToList();
            logger.Info("customers.Count() = ", customers.Count());

            List<CompanyDTO> performers = CService.GetAll().ToList();
            logger.Info("performers.Count() = ", performers.Count());

            customers.Insert(0, new CompanyDTO { Name = "Выберите заказчика", Id = Guid.Empty });

            if (customer != null && customer != Guid.Empty)
            {
                q = q.Where(p => p.CompanyCustomer.Id == customer);
            }

            performers.Insert(0, new CompanyDTO { Name = "Выберите исполнителя", Id = Guid.Empty });

            if (performer != null && performer != Guid.Empty)
            {
                q = q.Where(p => p.CompanyPerformer.Id == performer);
            }

            var qPages = q.Skip((page - 1) * pageItems).Take(pageItems);
            PageModel pageModel = new PageModel { CurrentPage = page, PageItems = pageItems, TotalItems = q.Count() };

            return View("List", new ProjectIndexViewModel
            {
                Projects = q.ToList(),
                PageModel = pageModel,
                Customers = new SelectList(customers, "Id", "Name"),
            });
        }

        [HttpGet]
        public ActionResult Create()
        {
            List<CompanyDTO> customers = CService.GetAll().ToList();
            logger.Info("customers.Count() = ", customers.Count());
            customers.Insert(0, new CompanyDTO { Name = "Выберите заказчика", Id = Guid.Empty });

            List<CompanyDTO> performers = CService.GetAll().ToList();
            logger.Info("performers.Count() = ", performers.Count());
            performers.Insert(0, new CompanyDTO { Name = "Выберите исполнителя", Id = Guid.Empty });

            List<EmployeeDTO> chiefs = EService.GetAll().Where(p=>p.IsChief == true).ToList();
            logger.Info("chiefs.Count() = ", chiefs.Count());
            chiefs.Insert(0, new EmployeeDTO { SecondName = "Выберите руководителся", Id = Guid.Empty });

            return View(new ProjectCreateModel
            {
                Project = new ProjectDTO(),
                Customers = new SelectList(customers, "Id", "Name"),
                Performers = new SelectList(performers, "Id", "Name") ,
                Chiefs = new SelectList(chiefs, "Id", "SecondName")
            });
        }

        [HttpPost]
        public ActionResult Create(ContextModel model)
        {
            logger.Info(model.Project.Id);
            logger.Info(model.Project.Name);
            logger.Info(model.Project.CompanyCustomer.Id);
            logger.Info(model.Project.CompanyPerformer.Id);
            logger.Info(model.Project.ProjectChief.Id);
            logger.Info(model.Project.Priority);
            logger.Info(model.Project.Comment);

            if (ModelState.IsValid)
            {
                PService.Create(model.Project);
            }

            return RedirectToAction("Index");
        }

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
            List<CompanyDTO> customers = CService.GetAll().ToList();
            logger.Info("customers.Count() = ", customers.Count());
            customers.Insert(0, new CompanyDTO { Name = "Выберите заказчика", Id = Guid.Empty });

            List<CompanyDTO> performers = CService.GetAll().ToList();
            logger.Info("performers.Count() = ", performers.Count());
            performers.Insert(0, new CompanyDTO { Name = "Выберите исполнителя", Id = Guid.Empty });

            List<EmployeeDTO> chiefs = EService.GetAll().Where(p => p.IsChief == true).ToList();
            logger.Info("chiefs.Count() = ", chiefs.Count());
            chiefs.Insert(0, new EmployeeDTO { SecondName = "Выберите руководителся", Id = Guid.Empty });

            return View(new ProjectCreateModel
            {
                Project = project,
                Customers = new SelectList(customers, "Id", "Name"),
                Performers = new SelectList(performers, "Id", "Name"),
                Chiefs = new SelectList(chiefs, "Id", "SecondName")
            });
        }

        [HttpPost]
        public ActionResult Edit(ContextModel model)
        {
            logger.Info("Edit() called Post");
            logger.Debug("model.Id: " + model.Project.Id);

            PService.Edit(model.Project);
            return RedirectToAction("Details", new { Id = model.Project.Id });
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

            var employee = EService.GetAll().FirstOrDefault(e => e.Id == employeeId);
            var project = PService.GetAll().FirstOrDefault(p => p.Id == projectId);

            var participant = PartService.GetAll().Where(p => p.Project.Id == projectId).FirstOrDefault(e => e.Employee.Id == employeeId);
            logger.Debug("participant.Employee.Id = " + participant.Employee.Id);
            logger.Debug("participant.Project.Id = " + participant.Project.Id);
            logger.Debug("participant.Id = " + participant.Id);

            PartService.Delete(participant.Id);

            logger.Info("participant removed");

            return Json(null, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AddParticipant(Guid employeeId, Guid projectId)
        {
            logger.Info("AddParticipant() called");
            logger.Debug("id = " + employeeId);
            logger.Debug("projectId = " + projectId);

            var participantDTO = new ParticipantDTO
            {
                Employee = EService.Find(employeeId),
                Project = PService.Find(projectId)
            };
            PartService.Create(participantDTO);

            logger.Info("participant added");

            return Json(null, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetParticipants(Guid projectId)
        {
            logger.Info("GetParticipants() called");
            logger.Debug("projectId = " + projectId);

            var participants = PartService.GetAll().Where(p => p.Project.Id == projectId).ToList();
            logger.Debug("participants.Count() = " + participants.Count());

            if (participants != null)
            {
                logger.Info(participants.Count());

                List<ParticipantsJsonModel> participantsList = participants
                    .Select(
                        entity =>
                        new ParticipantsJsonModel
                        {
                            id = entity.Employee.Id.ToString(),
                            name = entity.Employee.SecondName,
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