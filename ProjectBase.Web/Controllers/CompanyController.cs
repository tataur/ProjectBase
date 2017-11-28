using NLog;
using ProjectBase.DAL.DBContext;
using ProjectBase.DAL.Entities.Company;
using ProjectBase.Web.Models.Company;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectBase.Web.Controllers
{
    public class CompanyController : Controller
    {
        private static Logger logger = LogManager.GetLogger("Companies");
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
            var q = Context.Companies;
            logger.Debug("Companies: " + q.Count());

            return View("List", new CompanyListModel
            {
                Companies = q.ToList()
            });
        }

        [HttpGet]
        public ActionResult Create()
        {
            logger.Info("Create() called Get");
            return View(new CompanyModel());
        }

        [HttpPost]
        public ActionResult Create(CompanyModel model)
        {
            logger.Info("Create() called Post");
            logger.Debug("model.Id: " + model.Id);
            logger.Debug("model.Name: " + model.Name);
            logger.Debug("model.IsCustomer: " + model.IsCustomer);

            if (ModelState.IsValid)
            {
                var company = new CompanyEntity
                {
                    Id = model.Id,
                    Name = model.Name,
                    IsCustomer = model.IsCustomer
                };
                company.FillFieldsOnCreate();
                Context.Companies.Add(company);
                Context.SaveChanges();
                logger.Info("Company added");
            }

            return RedirectToAction("Index");
        }

        public ActionResult Details(Guid Id)
        {
            logger.Info("Details() called");
            logger.Info("Id: " + Id);

            var company = Context.Companies.FirstOrDefault(c => c.Id == Id);
            var model = new CompanyModel
            {
                Id = company.Id,
                Name = company.Name,
                IsCustomer = company.IsCustomer
            };
            return View(model);
        }

        [HttpGet]
        public ActionResult Edit(Guid Id)
        {
            logger.Info("Edit() called Get");
            logger.Info("Id: " + Id);

            var company = Context.Companies.FirstOrDefault(c => c.Id == Id);
            logger.Info("company.Id: " + company.Id);

            var model = new CompanyModel
            {
                Id = company.Id,
                Name = company.Name,
                IsCustomer = company.IsCustomer
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(CompanyModel model)
        {
            logger.Info("Edit() called Post");
            logger.Debug("model.Id: " + model.Id);
            logger.Debug("model.Name: " + model.Name);
            logger.Debug("model.IsCustomer: " + model.IsCustomer);

            var company = Context.Companies.FirstOrDefault(c => c.Id == model.Id);

            if (ModelState.IsValid && company != null)
            {
                company.Id = model.Id;
                company.Name = model.Name;
                company.IsCustomer = model.IsCustomer;

                Context.SaveChanges();
            }
            return RedirectToAction("Details", new { Id = company.Id });
        }
    }
}