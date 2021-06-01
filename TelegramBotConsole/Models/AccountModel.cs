using System;
using System.Collections.Generic;
using System.Text;

namespace TelegramBotConsole.Models
{
    class AccountModel
    {
        public bool canTrade { get; set; }
        public bool canWidthdraw { get; set; }
        public string accountType { get; set; }
        public Balance[] balances { get; set; }
    }
    public class Balance
    {
        public string asset { get; set; }
        public decimal free { get; set; }
        public decimal locked { get; set; }
    }
}
