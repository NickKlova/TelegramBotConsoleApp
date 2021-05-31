using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBotConsole.Keyboards.SettingsBlock
{
    class OrderInformationKeyboard
    {
        public static InlineKeyboardMarkup FirstStep = new InlineKeyboardMarkup(new[]
        {
            new []
            {
                InlineKeyboardButton.WithCallbackData("Information about a specific order","SpecificOrder"),
                InlineKeyboardButton.WithCallbackData("Information about open orders","AllOpenOrders"),
                InlineKeyboardButton.WithCallbackData("Information about all orders","AllOrders")
            }
        });
    }
}
