using System;
using System.Collections.Generic;
using System.Text;

namespace TelegramBotConsole.RequestBodyJson
{
    class OrderInformationRequest
    {
        public string symbol { get; set; }
        public int orderId { get; set; }
    }
}
