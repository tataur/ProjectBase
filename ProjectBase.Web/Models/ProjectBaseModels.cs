using ProjectBase.Logic.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ProjectBase.Web.Models
{
    public class EmployeeModel
    {
        public EmployeeDTO Employee { get; set; }
        public List<EmployeeDTO> Employees { get; set; }
    }

    public class ProjectModel
    {
        public ProjectDTO Project { get; set; }
        public List<ProjectDTO> Projects { get; set; }
    }

    public class EmployeeIndexViewModel
    {
        public IEnumerable<EmployeeDTO> Employees { get; set; }
        public PageModel PageModel { get; set; }
    }

    public class ProjectIndexViewModel
    {
        public IEnumerable<ProjectDTO> Projects { get; set; }
        public PageModel PageModel { get; set; }
        public SelectList Customers { get; set; }
        public SelectList Performers { get; set; }
        public SelectList Chiefs { get; set; }
        public SelectList Priority { get; set; }
        public string SortDir { get; set; }
        public string SortField { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        public DateTime CloseDate { get; set; }
    }

    public class ProjectCreateModel
    {
        public ProjectDTO Project { get; set; }
        public SelectList Customers { get; set; }
        public SelectList Performers { get; set; }
        public SelectList Chiefs { get; set; }
    }

    public class WorkersJsonModel
    {
        public string id { get; set; }
        public string name { get; set; }
    }
}