using NLog;
using ProjectBase.DAL.DBContext;
using ProjectBase.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectBase.Web.Controllers
{
    public class AutocompleteController : Controller
    {
        private static Logger logger = LogManager.GetLogger("Autocomplete");
        private readonly EFProjectBaseContext Context = new EFProjectBaseContext();

        public JsonResult CompaniesAutocomplete(string word)
        {
            logger.Info("CompaniesAutocomplete called");
            logger.Debug("word = " + word);
            word = (word ?? "");

            var companies = Context.Companies.Where(c => c.Name.Contains(word)).OrderBy(c => c.Name);

            logger.Info(companies.Count());
            List<AutocompleteJsonModel> companiesList = companies.Select(entity => new AutocompleteJsonModel { id = "" + entity.Id, value = entity.Name, label = entity.Name }).ToList();

            return Json(companiesList.ToArray(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult EmployeesAutocomplete(string word)
        {
            logger.Info("EmployeeAutocomplete called");
            logger.Debug("word = " + word);
            word = (word ?? "");

            var employees = Context.Employees.Where(e => e.SecondName.Contains(word)).OrderBy(c => c.SecondName);

            logger.Info(employees.Count());
            List<AutocompleteJsonModel> employeesList = employees.Select(entity => new AutocompleteJsonModel { id = "" + entity.Id, value = entity.SecondName, label = entity.SecondName}).ToList();

            return Json(employeesList.ToArray(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult ParticipantsAutocomplete(string projectId)
        {
            logger.Info("ParticipantsAutocomplete called");
            logger.Info("projectId = " + projectId);

            var employees = Context.Employees.OrderBy(c => c.SecondName);
            logger.Info("employees.Count() = " + employees.Count());
            var participants = Context.Participants.Where(p => p.Project.Id.ToString() == projectId);
            logger.Info("participants.Count() = " + participants.Count());

            List<AutocompleteJsonModel> employeesList = employees.Select(entity => new AutocompleteJsonModel { id = "" + entity.Id, value = entity.SecondName, label = entity.SecondName }).ToList();

            return Json(employeesList.ToArray(), JsonRequestBehavior.AllowGet);
        }
    }
}