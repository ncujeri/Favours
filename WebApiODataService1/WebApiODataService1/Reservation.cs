namespace WebApiODataService1
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Reservation
    {
        public int Id { get; set; }

        public int? Room_Id { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? Check_In { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? Check_Out { get; set; }

        [Column(TypeName = "money")]
        public decimal? Amount_Due { get; set; }

        [Column(TypeName = "money")]
        public decimal? Amount_Paid { get; set; }

        public int? Reservation_Satus { get; set; }

        public int? Number_Of_Adults { get; set; }

        public int? Number_Of_Children { get; set; }

        [StringLength(255)]
        public string Special_Requests { get; set; }
    }
}
