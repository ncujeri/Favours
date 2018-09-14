using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LaxFavours.Models.Dtos
{
    public class CustomerDetail
    {
        public int id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string email { get; set; }
        public decimal contact { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
    }
}