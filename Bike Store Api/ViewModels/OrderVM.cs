using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bike_Store_Api.ViewModels
{
    public class OrderVM
    {
        public int order_id { get; set; }
        public Nullable<int> customer_id { get; set; }
        public string first_name { get; set; }
        public byte order_status { get; set; }
        public System.DateTime order_date { get; set; }
        public System.DateTime required_date { get; set; }
        public Nullable<System.DateTime> shipped_date { get; set; }
        public int store_id { get; set; }
        public string store_name { get; set; }

        public int staff_id { get; set; }
        public string staff_first_name { get; set; }

       
    }
}