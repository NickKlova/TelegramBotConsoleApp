using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBotConsole.Keyboards
{
    class CurrentRateKeyboard
    {
        public static InlineKeyboardMarkup FirstStep = new InlineKeyboardMarkup(new[]
        {
            new []
            {
                InlineKeyboardButton.WithCallbackData("📤 Another coin 📤","AnotherCoinCurrentRate"),
            }
        });
    }
}
