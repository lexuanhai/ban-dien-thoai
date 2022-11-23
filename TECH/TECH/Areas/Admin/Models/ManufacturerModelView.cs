﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TECH.Areas.Admin.Models
{
    public class ManufacturerModelView
    {
        public int id { get; set; }
        public string? name { get; set; }
        public string? address { get; set; }
        public string? email { get; set; }
        public string? phone { get; set; }
        public int? status { get; set; }
        public DateTime created_at { get; set; }
        public DateTime? updated_at { get; set; }
    }
   
}
