using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TECH.SharedKernel;

namespace TECH.Data.DatabaseEntity
{
    [Table("specifications_category")]
    public class SpecificationsCategory : DomainEntity<int>
    {
        [Column(TypeName = "nvarchar(250)")]
        public string? name { get; set; }
        public int? status { get; set; }     
    }
}
