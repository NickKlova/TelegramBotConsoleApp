using System;
using System.Collections.Generic;
using System.Text;

namespace TelegramBotConsole.Models
{
    class ExchangeInfoModel
    {
        public Data[] data { get; set; }
    }
    public class Data
    {
        public string exchangeId { get; set; }
        public string baseId { get; set; }
        public string quoteId { get; set; }
        public string baseSymbol { get; set; }
        public string quoteSymbol { get; set; }
        public string volumeUsd24Hr { get; set; }
        public string priceUsd { get; set; }
    }
}
