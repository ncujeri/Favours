using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LaxFavours.Models.Dtos
{
    public class MerchantService
    {
        public int id { get; set; }
        public int merchant_details_id { get; set; }
        public string service_name { get; set; }
        public string short_description { get; set; }
        public string full_description { get; set; }
        public string logistics { get; set; }
        public decimal price { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public string state { get; set; }
    }
}