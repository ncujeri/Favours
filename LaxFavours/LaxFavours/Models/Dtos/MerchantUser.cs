using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LaxFavours.Models.Dtos
{
    public class MerchantUser
    {
        public int id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string email { get; set; }
        public string contant { get; set; }
        public string password { get; set; }
        public string type { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public string state { get; set; }

    }
}