using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TECH.SharedKernel;

namespace TECH.Data.DatabaseEntity
{
    [Table("color_images")]
    public class ColorImages : DomainEntity<int>
    {
        public int? product_quantities_id { get; set; }
        [ForeignKey("product_quantities_id")]
        public ProductQuantity? ProductQuantity { get; set; }
        public int? image_id { get; set; }
        [ForeignKey("image_id")]
        public Images? Images { get; set; }
    }
}
