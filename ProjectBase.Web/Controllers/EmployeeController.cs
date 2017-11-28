using ProjectBase.DAL.DBContext;
using ProjectBase.DAL.Entities.Employee;
using ProjectBase.Web.Models.Employee;
using System;
using System.Linq;
using System.Web.Mvc;
using NLog;

namespace ProjectBase.Web.Controllers
{
    public class EmployeeController : Controller
    {
        private static Logger logger = LogManager.GetLogger("Employees");
        private readonly EFProjectBaseContext Context = new EFProjectBaseContext();
        // GET: Employee
        public ActionResult Index()
        {
            logger.Info("Index() called");
            return List();
        }

        public ActionResult List()
        {
            logger.Info("List() called");

            var q = Context.Employees;
            logger.Debug("Employees:" + q.Count());

            return View("List", new EmployeeListModel
            {
                Employees = q.ToList()
            });
        }

        [HttpGet]
        public ActionResult Create()
        {
            logger.Info("Create() called Get");
            return View(new EmployeeModel());
        }

        [HttpPost]
        public ActionResult Create(EmployeeModel model)
        {
            logger.Info("Create() called Post");
            logger.Debug("model.Id: " + model.Id);
            logger.Debug("model.FirstName: " + model.FirstName);
            logger.Debug("model.SecondName: " + model.SecondName);
            logger.Debug("model.Patronymic: " + model.Patronymic);
            logger.Debug("model.Email: " + model.Email);
            logger.Debug("model.IsChief: " + model.IsChief);

            if (ModelState.IsValid)
            {
                var employee = new EmployeeEntity
                {
                    FirstName = model.FirstName,
                    SecondName = model.SecondName,
                    Patronymic = model.Patronymic,
                    Email = model.Email,
                    IsChief = model.IsChief
                };
                employee.FillFieldsOnCreate();
                Context.Employees.Add(employee);
                Context.SaveChanges();
                logger.Info("Employee added");
            }

            return RedirectToAction("Index");
        }

        public ActionResult Details(Guid Id)
        {
            logger.Info("Details() called");
            logger.Info("Id: " + Id);

            var employee = Context.Employees.FirstOrDefault(e => e.Id == Id);
            logger.Info("employee.Id: " + employee.Id);

            var model = new EmployeeModel
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                SecondName = employee.SecondName,
                Patronymic = employee.Patronymic,
                IsChief = employee.IsChief
            };
            return View(model);
        }

        [HttpGet]
        public ActionResult Edit(Guid Id)
        {
            logger.Info("Edit() called Get");
            logger.Info("Id: " + Id);

            var employee = Context.Employees.FirstOrDefault(e => e.Id == Id);
            var model = new EmployeeModel
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                SecondName = employee.SecondName,
                Patronymic = employee.Patronymic,
                IsChief = employee.IsChief
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(EmployeeModel model)
        {
            logger.Info("Edit() called Post");
            logger.Debug("model.Id: " + model.Id);
            logger.Debug("model.FirstName: " + model.FirstName);
            logger.Debug("model.SecondName: " + model.SecondName);
            logger.Debug("model.Patronymic: " + model.Patronymic);
            logger.Debug("model.Email: " + model.Email);
            logger.Debug("model.IsChief: " + model.IsChief);
            var employee = Context.Employees.FirstOrDefault(e => e.Id == model.Id);

            if (ModelState.IsValid && employee != null)
            {
                employee.Id = model.Id;
                employee.FirstName = model.FirstName;
                employee.SecondName = model.SecondName;
                employee.Patronymic = model.Patronymic;
                employee.Email = model.Email;
                employee.IsChief = model.IsChief;
                
                Context.SaveChanges();
            }
            return RedirectToAction("Details", new { Id = employee.Id });
        }
    }
}