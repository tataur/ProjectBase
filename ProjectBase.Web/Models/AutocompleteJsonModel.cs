using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectBase.Web.Models
{
    public class AutocompleteJsonModel
    {
        public string id { get; set; }
        public string companyId { get; set; }
        public string value { get; set; }
        public string label { get; set; }
    }
}