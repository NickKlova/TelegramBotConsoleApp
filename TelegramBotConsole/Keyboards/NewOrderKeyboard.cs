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
                InlineKeyboardButton.WithCallbackData("Other ❌", "coinCustom")
            }
        });

        public static InlineKeyboardMarkup SecondStep = new InlineKeyboardMarkup(new[]
        {
            new []
            {
                InlineKeyboardButton.WithCallbackData("⬆️ Buy","BUY"),
                InlineKeyboardButton.WithCallbackData("⬇️ Sell", "SELL")
            }
        });

        public static InlineKeyboardMarkup ThirdStep = new InlineKeyboardMarkup(new[]
        {
            new []
            {
                InlineKeyboardButton.WithCallbackData("50$","50$"),
                InlineKeyboardButton.WithCallbackData("30$", "30$"),
                InlineKeyboardButton.WithCallbackData("20$", "20$")

            },
            new[]
            {
                InlineKeyboardButton.WithCallbackData("10$","10$"),
                InlineKeyboardButton.WithCallbackData("Other 💵", "customPrice")
            }
        });
    }
}
