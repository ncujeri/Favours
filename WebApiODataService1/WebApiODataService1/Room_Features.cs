namespace WebApiODataService1
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Room_Features
    {
        public int Id { get; set; }

        public int? Room_ID { get; set; }

        public int? Features_Id { get; set; }
    }
}
