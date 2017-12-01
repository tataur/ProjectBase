using ProjectBase.Logic.DTO;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ProjectBase.Web.Models
{
    public class PageModel
    {
        public int CurrentPage { get; set; }
        public int PageItems { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages
        {
            get { return (int)Math.Ceiling((decimal)TotalItems / PageItems); }
        }
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
        public SelectList Priority { get; set; }
    }

    public class ProjectCreateModel
    {
        public ProjectDTO Project { get; set; }
        public SelectList Customers { get; set; }
        public SelectList Performers { get; set; }
        public SelectList Chiefs { get; set; }
    }
}