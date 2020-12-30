using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_Web.Models
{
    public class RevenueofStore
    {
        public string StoreName { get; set; }
        public string Address { get; set; }
        public string FullName { get; set; }
        public float Revenue { get; set; }
        public DateTime Time{get; set;}

    }
}