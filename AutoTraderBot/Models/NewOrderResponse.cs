using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AutoTraderBot.Models
{
    class NewOrderResponse
    {
        public string symbol { get; set; }
        public decimal price { get; set; }
        public string status { get; set; }
        public string side { get; set; }
        public long orderId { get; set; }
        public decimal buyedprice { get; set; }
       
    }

}
