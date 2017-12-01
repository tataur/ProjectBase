using System;
using System.Linq;
using System.Web.Mvc;
using NLog;
using ProjectBase.Logic.DTO;
using ProjectBase.Logic.Services;
using ProjectBase.Web.Models;

namespace ProjectBase.Web.Controllers
{
    public class EmployeeController : Controller
    {
        private static Logger logger = LogManager.GetLogger("Employees");
        private readonly EmployeeService EService = new EmployeeService();

        // GET: Employee
        public ActionResult Index()
        {
            logger.Info("Index() called");
            return List();
        }

        public ActionResult List(int page = 1)
        {
            logger.Info("List() called");
            int pageItems = 10;

            var employees = EService.GetAll();
            var employeesPages = employees.Skip((page - 1) * pageItems).Take(pageItems);
            PageModel pageModel = new PageModel { CurrentPage = page, PageItems = pageItems, TotalItems = employees.Count() };

            return View("List", new EmployeeIndexViewModel
            {
                Employees = employeesPages.ToList(),
                PageModel = pageModel
            });
        }

        [HttpGet]
        public ActionResult Create()
        {
            logger.Info("Create() called Get");
            return View(new ContextModel());
        }

        [HttpPost]
        public ActionResult Create(ContextModel model)
        {
            logger.Info("Create() called Post");

            if (ModelState.IsValid)
            {
                EService.Create(model.Employee);
                logger.Info("Employee added");
            }

            return RedirectToAction("Index");
        }

        public ActionResult Details(Guid Id)
        {
            logger.Info("Details() called");
            logger.Info("Id: " + Id);

            var employee = EService.Find(Id);
            logger.Info("employee.Id: " + employee.Id);
            ContextModel model = CreateEmployeeModel(employee);
            return View(model);
        }

        [HttpGet]
        public ActionResult Edit(Guid Id)
        {
            logger.Info("Edit() called Get");
            logger.Info("Id: " + Id);

            var employee = EService.Find(Id);
            ContextModel model = CreateEmployeeModel(employee);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(ContextModel model)
        {
            logger.Info("Edit() called Post");

            if (ModelState.IsValid)
            {
                EService.Edit(model.Employee);
            }
            return RedirectToAction("Details", new { Id = model.Employee.Id });
        }

        [HttpGet]
        public ActionResult Delete(Guid Id)
        {
            var employee = EService.Find(Id);
            ContextModel model = CreateEmployeeModel(employee);
            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(ContextModel model)
        {
            try
            {
                EService.Delete(model.Employee.Id);
            }
            catch(Exception ex)
            {
                return View("ErrorView", new ErrorModel { Title = "Невозможно удалить", Message = ex.Message });
            }
            return RedirectToAction("Index");
        }

        private static ContextModel CreateEmployeeModel(EmployeeDTO employee)
        {
            var employeeModel = new ContextModel
            {
                Employee = new EmployeeDTO
                {
                    Id = employee.Id,
                    FirstName = employee.FirstName,
                    SecondName = employee.SecondName,
                    Patronymic = employee.Patronymic,
                    Email = employee.Email,
                    IsChief = employee.IsChief
                }
            };

            logger.Info("CreateEmployeeModel: employeeModel.Employee.Id = " + employeeModel.Employee.Id);

            return employeeModel;
        }
    }
}