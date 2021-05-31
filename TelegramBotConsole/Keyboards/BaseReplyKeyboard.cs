using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBotConsole.Keyboards
{
    static class BaseReplyKeyboard
    {
        public static ReplyKeyboardMarkup BaseKeyboard = new ReplyKeyboardMarkup(
        new[] {
        new[]{
            new KeyboardButton("📊 Account"),
            new KeyboardButton("💳 New order"),
            new KeyboardButton("💼 Current rate")
        },
        new[]{
            new KeyboardButton("⚙️ Settings"),
            new KeyboardButton("❌ Delete order"),
            new KeyboardButton("〽️ Daily statistics")
        },
        }, true
        );
    }
}
