namespace WebApiODataService1
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Review
    {
        public int Id { get; set; }

        public int? Hotel_Id { get; set; }

        [Column("Review")]
        [StringLength(125)]
        public string Review1 { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? Posted_On { get; set; }

        public double? Rating { get; set; }

        [StringLength(255)]
        public string Reviewer_Name { get; set; }

        [Column(TypeName = "timestamp")]
        [MaxLength(8)]
        [Timestamp]
        public byte[] SSMA_TimeStamp { get; set; }
    }
}
