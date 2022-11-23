using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using TECH.Data.DatabaseEntity;

namespace TECH.Areas.Admin.Models
{
    public class ColorImagesModelView
    {
        public int id { get; set; }
        public int? product_quantities_id { get; set; }
        public int? image_id { get; set; }
    }
   
}
