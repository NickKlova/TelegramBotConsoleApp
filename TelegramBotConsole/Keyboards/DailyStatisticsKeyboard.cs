using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBotConsole.Keyboards
{
    class DailyStatisticsKeyboard
    {
        public static InlineKeyboardMarkup FirstStep = new InlineKeyboardMarkup(new[]
        {
            new []
            {
                InlineKeyboardButton.WithCallbackData("Another coin ...","AnotherCoinDailyStatistics"),
            }
        });
    }
}
