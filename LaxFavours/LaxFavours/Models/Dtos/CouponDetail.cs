using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LaxFavours.Models.Dtos
{
    public class CouponDetail
    {
        public int id { get; set; }
        public int coupon_code { get; set; }
        public string coupon_name { get; set; }
        public string coupon_type { get; set; }
        public string  value { get; set; }
        public string state { get; set; }
        public DateTime start_period { get; set; }
        public DateTime end_period { get; set; }
        public DateTime created_at { get; set; }

    }
}