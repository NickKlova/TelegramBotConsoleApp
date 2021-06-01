using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;

namespace TelegramBotConsole.Properties
{
    internal static class Tools
    {
        internal static async void CallbackQueryCoins(CallbackQueryEventArgs e, Commands.NewOrderCommand NewOrder, string coin, string buf)
        {
            await Properties.Config.client.DeleteMessageAsync(chatId: e.CallbackQuery.Message.Chat, e.CallbackQuery.Message.MessageId);

            NewOrder.json_order.symbol = coin;

            await Properties.Config.client.SendTextMessageAsync(
            chatId: e.CallbackQuery.Message.Chat,
            text: buf,
            parseMode: ParseMode.Markdown,
            disableNotification: true);

            await Properties.Config.client.SendTextMessageAsync(
            chatId: e.CallbackQuery.Message.Chat,
            text: $"Specify the type of transaction (⬆️Buy / ⬇️Sell):",
            parseMode: ParseMode.Markdown,
            disableNotification: true,
            replyMarkup: Keyboards.NewOrderKeyboard.SecondStep);
        }
        internal static async void CallbackQuerySide(CallbackQueryEventArgs e, Commands.NewOrderCommand NewOrder, string side, string buf)
        {
            await Properties.Config.client.DeleteMessageAsync(chatId: e.CallbackQuery.Message.Chat, e.CallbackQuery.Message.MessageId);

            NewOrder.json_order.side = side;

            await Properties.Config.client.SendTextMessageAsync(
            chatId: e.CallbackQuery.Message.Chat,
            text: buf,
            parseMode: ParseMode.Markdown,
            disableNotification: true);

            await Properties.Config.client.SendTextMessageAsync(
            chatId: e.CallbackQuery.Message.Chat,
            text: $"💰 Enter the amount:",
            parseMode: ParseMode.Markdown,
            disableNotification: true,
            replyMarkup: Keyboards.NewOrderKeyboard.ThirdStep);
        }
        internal static async void CallbackQueryQuantity(CallbackQueryEventArgs e, Commands.NewOrderCommand NewOrder, decimal quantity, string buf)
        {
            await Properties.Config.client.DeleteMessageAsync(chatId: e.CallbackQuery.Message.Chat, e.CallbackQuery.Message.MessageId);

            NewOrder.json_order.quantity = quantity;

            await Properties.Config.client.SendTextMessageAsync(
            chatId: e.CallbackQuery.Message.Chat,
            text: buf,
            parseMode: ParseMode.Markdown,
            disableNotification: true);

            await NewOrder.Execute(e.CallbackQuery.Message.Chat.Id);
        }
        internal static string GetCurrentRate(Models.ExchangeInfoModel response, string symbol,string symbolbuf = null)
        {
            if (symbolbuf == null)
                symbolbuf = symbol;

            string buf = $"{symbolbuf} price:\n\n";
            
            for (int i = 0; i < response.data.Length; i++)
            {
                if (response.data[i].exchangeId == "Binance" || response.data[i].exchangeId == "Huobi" || response.data[i].exchangeId == "Kucoin" || response.data[i].exchangeId == "Coinbase Pro")
                {
                    if (response.data[i].baseId == symbol && response.data[i].quoteSymbol == "USDT")
                    {
                        buf += $"🆔 Stock exchange: {response.data[i].exchangeId}\n" +
                            $"24 hour's volume: {response.data[i].volumeUsd24Hr}$\n" +
                            $"Price in dollars: {response.data[i].priceUsd}$\n\n\n";
                    }
                }
            }

            return buf;
        }
    }
}
