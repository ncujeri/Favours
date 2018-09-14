using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LaxFavours.Models.Dtos
{
    public class CustomerAddress
    {
        public int ID { get; set; }
        public int customer_details_id { get; set; }
        public string address_type { get; set; }
        public  string value { get; set; }
        public string state { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }


    }
}