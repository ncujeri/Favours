namespace WebApiODataService1
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Hotel
    {
        public int Id { get; set; }

        [StringLength(255)]
        public string Hotel_Name { get; set; }

        [StringLength(255)]
        public string Description { get; set; }

        [StringLength(10)]
        public string Hotel_Class { get; set; }

        public int? Room_Count { get; set; }

        [StringLength(3)]
        public string Location_Rating { get; set; }

        public double? Avg_Customer_Rating { get; set; }

        public double? Our_Rating { get; set; }

        [StringLength(255)]
        public string Address { get; set; }

        [StringLength(25)]
        public string Postal_Code { get; set; }

        [StringLength(25)]
        public string Phone { get; set; }

        [StringLength(100)]
        public string Website { get; set; }

        [StringLength(255)]
        public string Metro_Area { get; set; }

        [Column(TypeName = "timestamp")]
        [MaxLength(8)]
        [Timestamp]
        public byte[] SSMA_TimeStamp { get; set; }

        public int? City_Id { get; set; }
    }
}
