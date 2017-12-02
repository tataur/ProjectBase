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

        ProjectService service;

        public ProjectController()
        {
            service = new ProjectService();
        }

        public ProjectController(ProjectService serv)
        {
            service = serv;
        }

        public ActionResult Index(Guid? customer, Guid? performer, Guid? chief, int? priority, int page = 1)
        {
            return List(customer, performer, chief, priority, page);
        }

        public ActionResult List(Guid? customer, Guid? performer, Guid? chief, int? priority, int page = 1)
        {
            int pageItems = 10;

            IQueryable<ProjectDTO> q = service.GetAll().AsQueryable();
            List<CompanyDTO> customers = service.GetCompanies().ToList();
            List<CompanyDTO> performers = service.GetCompanies().ToList();
            List<EmployeeDTO> chiefs = service.GetEmployees().Where(c => c.IsChief == true).ToList();

            customers.Insert(0, new CompanyDTO { Name = "Все", Id = Guid.Empty });

            if (customer != null && customer != Guid.Empty)
            {
                q = q.Where(p => p.CompanyCustomer.Id == customer);
            }

            performers.Insert(0, new CompanyDTO { Name = "Все", Id = Guid.Empty });

            if (performer != null && performer != Guid.Empty)
            {
                q = q.Where(p => p.CompanyPerformer.Id == performer);
            }

            chiefs.Insert(0, new EmployeeDTO { SecondName = "Все", Id = Guid.Empty });

            if (chief != null && chief != Guid.Empty)
            {
                q = q.Where(c => c.ProjectChief.Id == chief);
            }

            var qPages = q.Skip((page - 1) * pageItems).Take(pageItems);
            PageModel pageModel = new PageModel { CurrentPage = page, PageItems = pageItems, TotalItems = q.Count() };

            return View("List", new ProjectIndexViewModel
            {
                Projects = q.ToList(),
                PageModel = pageModel,
                Customers = new SelectList(customers, "Id", "Name"),
                Performers = new SelectList(performers, "Id", "Name"),
                Chiefs = new SelectList(chiefs, "Id", "SecondName")
            });
        }

        [HttpGet]
        public ActionResult Create()
        {
            logger.Info("Create() called Get");

            List<CompanyDTO> customers = service.GetCompanies().ToList();
            customers.Insert(0, new CompanyDTO { Name = "Выберите заказчика", Id = Guid.Empty });

            List<CompanyDTO> performers = service.GetCompanies().ToList();
            performers.Insert(0, new CompanyDTO { Name = "Выберите исполнителя", Id = Guid.Empty });

            List<EmployeeDTO> chiefs = service.GetEmployees().Where(p=>p.IsChief == true).ToList();
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
        public ActionResult Create(ProjectModel model)
        {
            logger.Info("Create() called Post");

            if (ModelState.IsValid)
            {
                service.Create(model.Project);
            }
            else
            {
                return View("~/Views/Employee/ErrorView.cshtml", new ErrorModel { Title = "Проект не создан", Message = "" });
            }

            return RedirectToAction("Index");
        }

        public ActionResult Details(Guid Id)
        {
            logger.Info("Details() called");
            logger.Info("Id: " + Id);

            var project = service.Find(Id);
            ProjectModel model = CreateModel(project);
            return View(model);
        }

        [HttpGet]
        public ActionResult Edit(Guid Id)
        {
            logger.Info("Edit() called Get");
            logger.Info("Id: " + Id);

            var project = service.Find(Id);
            List<CompanyDTO> customers = service.GetCompanies().ToList();
            customers.Insert(0, new CompanyDTO { Name = "Выберите заказчика", Id = Guid.Empty });

            List<CompanyDTO> performers = service.GetCompanies().ToList();
            performers.Insert(0, new CompanyDTO { Name = "Выберите исполнителя", Id = Guid.Empty });

            List<EmployeeDTO> chiefs = service.GetEmployees().Where(p => p.IsChief == true).ToList();
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
        public ActionResult Edit(ProjectModel model)
        {
            logger.Info("Edit() called Post");
            logger.Debug("model.Id: " + model.Project.Id);

            service.Edit(model.Project);
            return RedirectToAction("Details", new { Id = model.Project.Id });
        }

        [HttpGet]
        public ActionResult Delete(Guid id)
        {
            var project = service.Find(id);
            var model = CreateModel(project);

            return View(model);
        }

        private static ProjectModel CreateModel(ProjectDTO project)
        {
            var projectModel = new ProjectModel
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

        public ActionResult AutocompleteSearch(string term)
        {
            logger.Info("AutocompleteSearch");

            var models = service.GetEmployees().Where(e => e.IsChief == false).Select(a => new { value = a.SecondName }).Distinct();
            
            return Json(models, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteWorker(Guid employeeId, Guid projectId)
        {
            logger.Info("DeleteWorker() called");
            logger.Debug("employeeId = " + employeeId);
            logger.Debug("projectId = " + projectId);

            var employee = service.GetEmployees().FirstOrDefault(e => e.Id == employeeId);
            var project = service.Find(projectId);

            //var worker = service.GetAll().Where(p => p.Project.Id == projectId).FirstOrDefault(e => e.Employee.Id == employeeId);
//            logger.Debug("participant.Employee.Id = " + worker.Employee.Id);
  //          logger.Debug("participant.Project.Id = " + worker.Project.Id);
    //        logger.Debug("participant.Id = " + worker.Id);

            //WService.Delete(worker.Id);

            logger.Info("participant removed");

            return Json(null, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AddWorker(Guid employeeId, Guid projectId)
        {
            logger.Info("AddWorker() called");
            logger.Debug("id = " + employeeId);
            logger.Debug("projectId = " + projectId);

            var workerDTO = new WorkerDTO
            {
                Employee = service.GetEmployees().FirstOrDefault(e => e.Id == employeeId),
                Project = service.Find(projectId)
            };
            //WService.Create(workerDTO);

            logger.Info("participant added");

            return Json(null, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetWorkers(Guid projectId)
        {
            logger.Info("GetWorkers() called");
            logger.Debug("projectId = " + projectId);

            var workers = service.GetWorkers().Where(p => p.Project.Id == projectId).ToList();
            logger.Debug("workers.Count() = " + workers.Count());

            if (workers != null)
            {
                logger.Info(workers.Count());

                List<WorkersJsonModel> workersList = workers
                    .Select(
                        entity =>
                        new WorkersJsonModel
                        {
                            id = entity.Employee.Id.ToString(),
                            name = entity.Employee.SecondName,
                        })
                    .ToList();
                return Json(workersList.ToArray(), JsonRequestBehavior.AllowGet);
            }
            else
            {
                logger.Info("нет участников");

                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }
    }
}