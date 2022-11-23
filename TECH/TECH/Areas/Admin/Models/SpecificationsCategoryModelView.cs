﻿using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using TECH.Data.DatabaseEntity;

namespace TECH.Areas.Admin.Models
{
    public class SpecificationsCategoryModelView
    {
        public int id { get; set; }
        public string? name { get; set; }
        public int? status { get; set; }
    }
   
}
