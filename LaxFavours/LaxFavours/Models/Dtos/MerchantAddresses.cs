using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LaxFavours.Models.Dtos
{
    public class MerchantAddresses
    {
        public int id { get; set; }
        public int merchant_details_id { get; set; }
        public string address_type { get; set; }
        public string  value { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public string state { get; set; }

    }
}