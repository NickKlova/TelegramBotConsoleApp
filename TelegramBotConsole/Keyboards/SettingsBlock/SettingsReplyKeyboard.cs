using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBotConsole.Keyboards.SettingsBlock
{
    static class SettingsReplyKeyboard
    {
        public static ReplyKeyboardMarkup BaseKeyboard = new ReplyKeyboardMarkup(
        new[] {
        new[]{
            new KeyboardButton("🌐 Order information"),
            new KeyboardButton("♻️ Change API keys"),
        },
        new[]{
            new KeyboardButton("📌 Support"),
            new KeyboardButton("⚠️ Back to main menu")
        },
        }, true
        );
    }
}
