using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using TECH.Data.DatabaseEntity;

namespace TECH.Areas.Admin.Models
{
    public class QuantityProductModelView
    {
        public int Id { get; set; }
        //public ProductModelView Product { get; set; }
        //public AppSizeModelView AppSize { get; set; }
        public int? TotalImported { get; set; }
        public int? ProductId { get; set; }
        public int? AppSizeId { get; set; }
        public int? ColorId { get; set; }
        //public int? TotalSold { get; set; }
        //public int? TotalStock { get; set; }
        //public DateTime? DateImport { get; set; }
        //public string DateImportStr { get; set; }
        //public string AppSizeStr { get; set; }
        //public bool IsDeleted { get; set; }

        // update start
        public int id { get; set; }
        public int? product_id { get; set; }
        public Products? Products { get; set; }

        public int? color_id { get; set; }
        public Colors? Colors { get; set; }
        public int? totalimport { get; set; }

        public decimal? priceimprot { get; set; }
        public decimal? pricesell { get; set; }
        public int? totalsell { get; set; }
        public int? totalinventory { get; set; }
        public int status { get; set; }
        // update end


    }
}
