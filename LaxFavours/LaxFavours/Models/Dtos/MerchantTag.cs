using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LaxFavours.Models.Dtos
{
    public class MerchantTag
    {
        public int id { get; set; }
        public int merchant_details_id { get; set; }
        public string tag_type { get; set; }
        public string tag_level { get; set; }
        public string tag_value { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
    }
}