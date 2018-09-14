using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LaxFavours.Models.Dtos
{
    public class MerchantOperatingTimes
    {
        public int id { get; set; }
        public int merchant_details_id { get; set; }
        public string operation_type { get; set; }
        public DateTime start_time { get; set; }
        public DateTime end_time { get; set; }
        public DateTime create_at { get; set; }
        public DateTime updated_at { get; set; }
        public string state { get; set; }
    }
}