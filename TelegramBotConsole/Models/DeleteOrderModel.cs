using System;
using System.Collections.Generic;
using System.Text;

namespace TelegramBotConsole.Models
{
    class DeleteOrderModel
    {
        public string symbol { get; set; }
        public long orderId { get; set; }
        public decimal price { get; set; }
        public string status { get; set; }
        public string type { get; set; }
        public string side { get; set; }
    }
}
