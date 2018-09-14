using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Web;
using LaxFavours.Models.Dtos;

namespace LaxFavours.Models
{
    [DbConfigurationType(typeof(MySql.Data.Entity.MySqlEFConfiguration))]
    public class FavoursDbContext : DbContext
    {
        public virtual DbSet<CustomerDetail> CustomerDetails { get; set; }
        public virtual DbSet<CustomerAddress> CustomeAddresses { get; set; }
        public virtual DbSet<CouponDetail> CouponDetails { get; set; }
        public virtual DbSet<MerchantDetail> MerchantDetails { get; set; }
        public virtual DbSet<MerchantImage> MerchantImages { get; set; }
        public virtual DbSet<MerchantService> MerchantServices { get; set; }
        public virtual DbSet<MerchantUser> MerchantUsers { get; set; }
        public virtual DbSet<MerchantTag> MerchantTags { get; set; }
        public virtual DbSet<MerchantOperatingTimes> MerchantOperatingTimes { get; set; }
        public virtual DbSet<MerchantBookingType> MerchantBookingTypes { get; set; }



        public FavoursDbContext() : base("FavoursDb") { }
        public FavoursDbContext(DbConnection existingConnection, bool contextOwnsConnection)
            : base(existingConnection, contextOwnsConnection)
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CustomerDetail>().ToTable("customer_details");
            modelBuilder.Entity<CustomerAddress>().Property(c => c.ID).HasColumnName("id");

            modelBuilder.Entity<CustomerAddress>().ToTable("customer_addresses");
            modelBuilder.Entity<CouponDetail>().ToTable("coupon_details");
            modelBuilder.Entity<MerchantAddresses>().ToTable("merchant_addresses");
            modelBuilder.Entity<MerchantBookingType>().ToTable("merchant_booking_type");
            modelBuilder.Entity<MerchantImage>().ToTable("merchant_images");
            modelBuilder.Entity<MerchantService>().ToTable("merchant_services");
            modelBuilder.Entity<MerchantTag>().ToTable("merchant_tags");
            modelBuilder.Entity<MerchantUser>().ToTable("merchant_users");


        }
    }  
    
}