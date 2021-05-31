using System;
using System.Collections.Generic;
using System.Text;

namespace TelegramBotConsole.RequestBodyJson
{
    class DeleteOrderRequest
    {
        public int orderId { get; set; }
        public string symbol { get; set; }
    }
}
