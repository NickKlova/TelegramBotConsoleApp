using System;
using System.Collections.Generic;
using System.Text;

namespace AutoTraderBot.Models
{
    class GetDailyRateModel
    {
        public string symbol { get; set; }
        public decimal priceChange { get; set; }
        public decimal lastPrice { get; set; }
        public decimal highPrice { get; set; }
        public decimal lowPrice { get; set; }
        public decimal volume { get; set; }
    }
}
