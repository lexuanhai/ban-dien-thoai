using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TECH.SharedKernel;

namespace TECH.Data.DatabaseEntity
{
    [Table("specifications")]
    public class Specifications : DomainEntity<int>
    {
        [Column(TypeName = "nvarchar(250)")]
        public string? name { get; set; }
        public int? specificationscategory_id { get; set; }
        [ForeignKey("specificationscategory_id")]
        public SpecificationsCategory? SpecificationsCategory { get; set; }
        public int? product_id { get; set; }
        [ForeignKey("product_id")]
        public Products? Products { get; set; }
        public int? status { get; set; }     
    }
}
