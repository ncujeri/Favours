namespace WebApiODataService1
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class City
    {
        public int ID { get; set; }

        [Column("City")]
        [StringLength(50)]
        public string City1 { get; set; }

        [StringLength(50)]
        public string StateProvince { get; set; }

        [StringLength(50)]
        public string Country { get; set; }

        [StringLength(100)]
        public string Offer { get; set; }

        [StringLength(100)]
        public string CIty_Image { get; set; }
    }
}
