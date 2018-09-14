namespace WebApiODataService1
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Room_Type
    {
        public int ID { get; set; }

        [StringLength(50)]
        public string RoomType { get; set; }
    }
}
