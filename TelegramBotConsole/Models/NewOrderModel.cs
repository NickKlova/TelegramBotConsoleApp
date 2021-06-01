using System;
using System.Collections.Generic;
using System.Text;

namespace TelegramBotConsole.Models
{
    class NewOrderModel
    {
        public string symbol { get; set; }
        public long orderId { get; set; }
        public string clientOrderId { get; set; }
        public decimal price { get; set; }
        public string side { get; set; }
    }
}
