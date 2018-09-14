namespace WebApiODataService1
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Picture
    {
        public int Id { get; set; }

        [StringLength(255)]
        public string Picture_Id { get; set; }

        public int? Hotel_Id { get; set; }

        public int? Room_Id { get; set; }
    }
}
