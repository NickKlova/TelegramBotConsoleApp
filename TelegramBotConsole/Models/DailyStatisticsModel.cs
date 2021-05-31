using System;
using System.Collections.Generic;
using System.Text;

namespace TelegramBotConsole.Models
{
    class DailyStatisticsModel
    {
        public string symbol { get; set; }
        public decimal priceChange { get; set; }
        public decimal lastPrice { get; set; }
        public decimal highPrice { get; set; }
        public decimal lowPrice { get; set; }
        public decimal volume { get; set; }
    }
}
