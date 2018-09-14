using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LaxFavours.Models.Dtos
{
    public class ServiceTransaction
    {
        public int id { get; set; }
        public string service_datails_id { get; set; }
        public string customer_details_id { get; set; }
        public string status { get; set; }
        public decimal actual_price { get; set; }
        public decimal discount_amount { get; set; }
        public decimal original_price { get; set; }
        public decimal total_amount { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
    }
}