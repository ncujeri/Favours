namespace WebApiODataService1
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Hotel_Images
    {
        public int ID { get; set; }

        public int? Hotel_ID { get; set; }

        [StringLength(100)]
        public string Image_Filename { get; set; }
    }
}