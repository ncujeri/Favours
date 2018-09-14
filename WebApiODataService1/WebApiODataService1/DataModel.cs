namespace WebApiODataService1
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class DataModel : DbContext
    {
        public DataModel()
            : base("name=DataModel")
        {
        }

        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<Features_List> Features_List { get; set; }
        public virtual DbSet<Guest> Guests { get; set; }
        public virtual DbSet<Hotel_Features> Hotel_Features { get; set; }
        public virtual DbSet<Hotel_Images> Hotel_Images { get; set; }
        public virtual DbSet<Hotel> Hotels { get; set; }
        public virtual DbSet<Picture> Pictures { get; set; }
        public virtual DbSet<Reservation> Reservations { get; set; }
        public virtual DbSet<Review> Reviews { get; set; }
        public virtual DbSet<Room_Features> Room_Features { get; set; }
        public virtual DbSet<Room_Type> Room_Type { get; set; }
        public virtual DbSet<Room> Rooms { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Features_List>()
                .Property(e => e.Icon)
                .IsFixedLength();

            modelBuilder.Entity<Hotel>()
                .Property(e => e.SSMA_TimeStamp)
                .IsFixedLength();

            modelBuilder.Entity<Reservation>()
                .Property(e => e.Check_In)
                .HasPrecision(0);

            modelBuilder.Entity<Reservation>()
                .Property(e => e.Check_Out)
                .HasPrecision(0);

            modelBuilder.Entity<Reservation>()
                .Property(e => e.Amount_Due)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Reservation>()
                .Property(e => e.Amount_Paid)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Review>()
                .Property(e => e.Posted_On)
                .HasPrecision(0);

            modelBuilder.Entity<Review>()
                .Property(e => e.SSMA_TimeStamp)
                .IsFixedLength();

            modelBuilder.Entity<Room>()
                .Property(e => e.Nighly_Rate)
                .HasPrecision(19, 4);
        }
    }
}
