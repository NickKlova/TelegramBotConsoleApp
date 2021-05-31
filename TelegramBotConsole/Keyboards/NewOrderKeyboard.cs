using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBotConsole.Keyboards
{
    class NewOrderKeyboard
    {
        public static InlineKeyboardMarkup FirstStep = new InlineKeyboardMarkup(new[]
        {
            new []
            {
                InlineKeyboardButton.WithCallbackData("BTC/USDT","BTCUSDT"),
                InlineKeyboardButton.WithCallbackData("ETH/USDT", "ETHUSDT")
            },
            new[]
            {
                InlineKeyboardButton.WithCallbackData("LTC/BTC","LTCBTC"),
                InlineKeyboardButton.WithCallbackData("Other...", "coinCustom")
            }
        });

        public static InlineKeyboardMarkup SecondStep = new InlineKeyboardMarkup(new[]
        {
            new []
            {
                InlineKeyboardButton.WithCallbackData("Buy","BUY"),
                InlineKeyboardButton.WithCallbackData("Sell", "SELL")
            }
        });

        public static InlineKeyboardMarkup ThirdStep = new InlineKeyboardMarkup(new[]
        {
            new []
            {
                InlineKeyboardButton.WithCallbackData("100%","100percent"),
                InlineKeyboardButton.WithCallbackData("75%", "75percent"),
                InlineKeyboardButton.WithCallbackData("50%", "50percent")

            },
            new[]
            {
                InlineKeyboardButton.WithCallbackData("25%","25percent"),
                InlineKeyboardButton.WithCallbackData("Other...", "customPercent")
            }
        });
    }
}
