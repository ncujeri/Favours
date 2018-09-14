namespace WebApiODataService1
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Guest
    {
        public int Id { get; set; }

        [StringLength(255)]
        public string First_Name { get; set; }

        [StringLength(255)]
        public string Last_Name { get; set; }

        [StringLength(255)]
        public string Title { get; set; }

        [StringLength(255)]
        public string Address { get; set; }

        [StringLength(255)]
        public string City { get; set; }

        [StringLength(255)]
        public string State { get; set; }

        [StringLength(255)]
        public string Postal_Code { get; set; }

        [StringLength(255)]
        public string Country { get; set; }

        [StringLength(255)]
        public string Phone_Number { get; set; }

        [StringLength(255)]
        public string Email { get; set; }

        [StringLength(255)]
        public string Password { get; set; }
    }
}
