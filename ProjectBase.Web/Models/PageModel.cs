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
    
}