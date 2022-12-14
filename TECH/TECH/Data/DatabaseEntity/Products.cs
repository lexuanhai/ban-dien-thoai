using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TECH.SharedKernel;

namespace TECH.Data.DatabaseEntity
{
    [Table("products")]
    public class Products : DomainEntity<int>
    {
        [Column(TypeName = "nvarchar(250)")]
        public string? name { get; set; }
        public int? category_id { get; set; }
        [ForeignKey("category_id")]
        public Category? Category { get; set; }

        public int? manufacturer_id { get; set; }
        [ForeignKey("manufacturer_id")]
        public Manufacturer? Manufacturer { get; set; }


        [Column(TypeName = "decimal(18,0)")]
        public decimal? price_sell { get; set; }

        [Column(TypeName = "decimal(18,0)")]
        public decimal? price_reduced { get; set; }
        [Column(TypeName = "decimal(18,0)")]
        public decimal? price_import { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public string? trademark { get; set; }        

        [Column(TypeName = "nvarchar(max)")]
        public string? description { get; set; }
        [Column(TypeName = "nvarchar(max)")]
        public string? promotion { get; set; }
        public int? percent_price { get; set; }
        public int? status { get; set; }
        [Column(TypeName = "nvarchar(max)")]
        public string? insurance { get; set; }
        public int? commodities { get; set; } // loại hàng hóa thể hiện là hàng chính hãng, hay là hàng cũ(%), hay là hàng lỗi kỹ thuật

    }
}
