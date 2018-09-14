using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LaxFavours.Models.Dtos
{
    public class MerchantDetail
    {
        public int id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string contant { get; set; }
        public string short_description { get; set; }
        public string full_description { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public string state { get; set; }

    }
}