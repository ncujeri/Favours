namespace WebApiODataService1
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Features_List
    {
        public int ID { get; set; }

        [StringLength(50)]
        public string Feature_Name { get; set; }

        [StringLength(50)]
        public string Icon { get; set; }
    }
}
