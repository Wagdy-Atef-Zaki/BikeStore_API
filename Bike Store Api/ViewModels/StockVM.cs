using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bike_Store_Api.ViewModels
{
    public class StockVM
    {
        public int store_id { get; set; }
        public string store_name { get; set; }
        public int product_id { get; set; }
        public string product_name { get; set; }
        public Nullable<int> quantity { get; set; }

    }
}