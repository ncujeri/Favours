namespace WebApiODataService1
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Room
    {
        public int Id { get; set; }

        public int? Hotel_Id { get; set; }

        public int? RoomType { get; set; }

        [StringLength(50)]
        public string Room_Short_Description { get; set; }

        [Column(TypeName = "money")]
        public decimal? Nighly_Rate { get; set; }

        [StringLength(50)]
        public string Room_Image1 { get; set; }

        [StringLength(50)]
        public string Room_Image2 { get; set; }

        [StringLength(50)]
        public string Room_Image3 { get; set; }

        [StringLength(50)]
        public string Room_Image4 { get; set; }

        [StringLength(50)]
        public string Room_image5 { get; set; }
    }
}
