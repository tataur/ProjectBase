using NLog;
using ProjectBase.Logic.Services;
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
        private readonly CompanyService CService = new CompanyService();
        private readonly EmployeeService EService = new EmployeeService();

        public JsonResult CompaniesAutocomplete(string word)
        {
            logger.Info("CompaniesAutocomplete called");
            logger.Debug("word = " + word);
            word = (word ?? "");

            var companies = CService.GetAll();

            logger.Info(companies.Count());
            List<AutocompleteJsonModel> companiesList = companies.Select(entity => new AutocompleteJsonModel { id = "" + entity.Id, value = entity.Name, label = entity.Name }).ToList();

            return Json(companiesList.ToArray(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult ChiefsAutocomplete(string word)
        {
            logger.Info("EmployeeAutocomplete called");
            logger.Debug("word = " + word);
            word = (word ?? "");

            var employees = EService.GetAll().Where(e=>e.IsChief == true);

            logger.Info(employees.Count());
            List<AutocompleteJsonModel> employeesList = employees.Select(entity => new AutocompleteJsonModel { id = "" + entity.Id, value = entity.SecondName, label = entity.SecondName}).ToList();

            return Json(employeesList.ToArray(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult ParticipantsAutocomplete(string word)
        {
            logger.Info("ParticipantsAutocomplete called");

            var employees = EService.GetAll().Where(e => e.IsChief == false);
            logger.Info("employees.Count() = " + employees.Count());
            //var participants = Context.Participants.Where(p => p.Project.Id.ToString() == projectId);
            //logger.Info("participants.Count() = " + participants.Count());

            List<AutocompleteJsonModel> employeesList = employees.Select(entity => new AutocompleteJsonModel { id = "" + entity.Id, value = entity.SecondName, label = entity.SecondName }).ToList();

            return Json(employeesList.ToArray(), JsonRequestBehavior.AllowGet);
        }
    }
}