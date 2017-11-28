//using ProjectBase.DAL.DBContext;
//using ProjectBase.DAL.Entities.Project;
//using ProjectBase.Web.Models.Project;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;

//namespace ProjectBase.Web.Controllers
//{
//    public class ProjectController : Controller
//    {
//        private readonly EFProjectBaseContext Context = new EFProjectBaseContext();
//        // GET: Employee
//        public ActionResult Index()
//        {
//            return List();
//        }

//        public ActionResult List()
//        {
//            var q = Context.Projects;

//            return View("List", new ProjectListModel
//            {
//                Projects = q.ToList()
//            });
//        }

//        [HttpGet]
//        public ActionResult Create()
//        {
//            return View(new ProjectModel());
//        }

//        [HttpPost]
//        public ActionResult Create(ProjectModel model)
//        {
//            if (ModelState.IsValid)
//            {
//                var employee = new ProjectEntity
//                {
//                    Name = model.Name,
//                    CompanyCustomer = model.CompanyCustomerId
//                };
//                employee.FillFieldsOnCreate();
//                Context.Employees.Add(employee);
//                Context.SaveChanges();
//            }

//            return RedirectToAction("Index");
//        }

//        public ActionResult Details(Guid Id)
//        {
//            var employee = Context.Employees.FirstOrDefault(e => e.Id == Id);
//            var model = new EmployeeModel
//            {
//                FirstName = employee.FirstName,
//                SecondName = employee.SecondName,
//                Patronymic = employee.Patronymic,
//                IsChief = employee.IsChief
//            };
//            return View(model);
//        }

//        [HttpGet]
//        public ActionResult Edit(Guid Id)
//        {
//            var employee = Context.Employees.FirstOrDefault(e => e.Id == Id);
//            var model = new EmployeeModel
//            {
//                FirstName = employee.FirstName,
//                SecondName = employee.SecondName,
//                Patronymic = employee.Patronymic,
//                IsChief = employee.IsChief
//            };
//            return View(model);
//        }
//    }
//}