using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TECH.SharedKernel;

namespace TECH.Data.DatabaseEntity
{
    [Table("manufacturer")]
    public class Manufacturer : DomainEntity<int>
    {
        [Column(TypeName = "nvarchar(250)")]
        public string? name { get; set; }
        [Column(TypeName = "nvarchar(250)")]
        public string? address { get; set; }
        [Column(TypeName = "varchar(200)")]
        public string? email { get; set; }
        [Column(TypeName = "varchar(20)")]
        public string? phone { get; set; }
        public int? status { get; set; }

        public DateTime created_at { get; set; }

        public DateTime? updated_at { get; set; }
    }
}
