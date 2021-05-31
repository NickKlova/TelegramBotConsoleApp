using System;
using System.Collections.Generic;
using System.Text;

namespace TelegramBotConsole.RequestBodyJson
{
    class NewOrderRequest
    {
        public string symbol { get; set; }
        public string side { get; set; }
        public string type { get; set; } = "MARKET";
        public decimal quantity { get; set; }
        public decimal price { get; set; }
    }
}
