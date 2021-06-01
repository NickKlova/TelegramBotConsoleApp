using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBotConsole
{
    class TelegramBot
    {
        Commands.NewOrderCommand NewOrder;
        Commands.DeleteOrderCommand DeleteOrder;
        Commands.SettingsBlock.AllOrderInformationCommand AllOrderInformation;
        public TelegramBot()
        {
            Properties.Config.client.OnMessage += ClientOnMessage;
            Properties.Config.client.OnCallbackQuery += ClientOnCallbackQuery;
            Properties.Config.client.StartReceiving();
        }

        private async void ClientOnCallbackQuery(object sender, CallbackQueryEventArgs e)
        {
            #region Coins
            if (e.CallbackQuery.Data == "BTCUSDT")
            {
                Properties.Tools.CallbackQueryCoins(e, NewOrder, "BTCUSDT", "You choose BTC to USDT!");
            }
            if (e.CallbackQuery.Data == "ETHUSDT")
            {
                Properties.Tools.CallbackQueryCoins(e, NewOrder, "ETHUSDT", "You choose ETH to USDT!");
            }

            if (e.CallbackQuery.Data == "coinCustom")
            {
                await Properties.Config.client.SendTextMessageAsync(
                chatId: e.CallbackQuery.Message.Chat,
                text: $"💷 Enter yor cryptocurrency pair:",
                parseMode: ParseMode.Markdown,
                disableNotification: true,
                replyMarkup: new ForceReplyMarkup() { Selective = true });
            }
            #endregion
            #region Side
            if (e.CallbackQuery.Data == "BUY")
            {
                Properties.Tools.CallbackQuerySide(e, NewOrder, "BUY", "Ok, I will buy!");
            }
            if (e.CallbackQuery.Data == "SELL")
            {
                Properties.Tools.CallbackQuerySide(e, NewOrder, "SELL", "Ok, I will sell!");
            }
            #endregion
            #region Quantity
            if (e.CallbackQuery.Data == "50$")
            {
                Properties.Tools.CallbackQueryQuantity(e, NewOrder, 50, "Then I will buy coins for this amount! ($ 50)");
            }
            if (e.CallbackQuery.Data == "30$")
            {
                Properties.Tools.CallbackQueryQuantity(e, NewOrder, 30, "Then I will buy coins for this amount! ($ 30)");
            }
            if (e.CallbackQuery.Data == "20$")
            {
                Properties.Tools.CallbackQueryQuantity(e, NewOrder, 20, "Then I will buy coins for this amount! ($ 20)");
            }
            if (e.CallbackQuery.Data == "10$")
            {
                Properties.Tools.CallbackQueryQuantity(e, NewOrder, 10, "Then I will buy coins for this amount! ($ 10)");
            }
            if (e.CallbackQuery.Data == "customPrice")
            {
                await Properties.Config.client.DeleteMessageAsync(chatId: e.CallbackQuery.Message.Chat, e.CallbackQuery.Message.MessageId);

                await Properties.Config.client.SendTextMessageAsync(
                chatId: e.CallbackQuery.Message.Chat,
                text: $"💰 Enter the amount:",
                parseMode: ParseMode.Markdown,
                disableNotification: true,
                replyMarkup: new ForceReplyMarkup() { Selective = true });
            }
            #endregion
            #region CurrentRate
            if (e.CallbackQuery.Data == "AnotherCoinCurrentRate")
            {
                var message = await Properties.Config.client.SendTextMessageAsync(
                                chatId: e.CallbackQuery.Message.Chat,
                                text: $"Enter the cryptocurrency pair to get the current rate on popular exchanges:",
                                parseMode: ParseMode.Markdown,
                                disableNotification: true,
                                replyMarkup: new ForceReplyMarkup() { Selective = true });
            }
            #endregion
            #region DailyStatistics
            if(e.CallbackQuery.Data == "AnotherCoinDailyStatistics")
            {
                var message = await Properties.Config.client.SendTextMessageAsync(
                                chatId: e.CallbackQuery.Message.Chat,
                                text: $"📤 Enter yor cryptocurrency coin:",
                                parseMode: ParseMode.Markdown,
                                disableNotification: true,
                                replyMarkup: new ForceReplyMarkup() { Selective = true });
            }
            #endregion
            #region SettingsBlock
            if(e.CallbackQuery.Data == "AllOrders")
            {
                await Properties.Config.client.SendTextMessageAsync(
                                chatId: e.CallbackQuery.Message.Chat,
                                text: $"Enter symbol for get all orders:",
                                parseMode: ParseMode.Markdown,
                                disableNotification: true,
                                replyMarkup: new ForceReplyMarkup() { Selective = true });
            }
            #endregion
        }

        private async void ClientOnMessage(object sender, MessageEventArgs e)
        {
            Console.WriteLine($"Username:{e.Message.From.Username}. Text:{e.Message.Text}");
            switch (e.Message.Text)
            {
                case "📊 Account":
                    Commands.AccountCommand command = new Commands.AccountCommand(e);
                    await command.Execute(e);
                    break;
                case "💳 New order":
                    NewOrder = new Commands.NewOrderCommand(e);
                    await Properties.Config.client.SendTextMessageAsync(
                    chatId: e.Message.Chat,
                    text: $"Choose cryptocurrency pair:",
                    parseMode: ParseMode.Markdown,
                    disableNotification: true,
                    replyMarkup: Keyboards.NewOrderKeyboard.FirstStep);
                    break;
                case "❌ Delete order":
                    DeleteOrder = new Commands.DeleteOrderCommand(e);
                    await Properties.Config.client.SendTextMessageAsync(
                    chatId: e.Message.Chat,
                    text: $"Enter cryptocurrency pair for deleting:",
                    parseMode: ParseMode.Markdown,
                    disableNotification: true,
                    replyMarkup: new ForceReplyMarkup() { Selective = true });
                    break;
                case "💼 Current rate":
                    Commands.CurrentRateCommand commandRate = new Commands.CurrentRateCommand();
                    await commandRate.Execute(e);
                    break;
                case "〽️ Daily statistics":
                    Commands.DailyStatisticsCommand commandDaily = new Commands.DailyStatisticsCommand(e);
                    await commandDaily.Execute(e);
                    break;
                case "⚙️ Settings":
                    await Properties.Config.client.SendTextMessageAsync(
                    chatId: e.Message.Chat,
                    text: "You are in setting's block!",
                    parseMode: ParseMode.Markdown,
                    disableNotification: true,
                    replyMarkup: Keyboards.SettingsBlock.SettingsReplyKeyboard.BaseKeyboard);
                    break;
                case "🌐 Order information":
                    await Properties.Config.client.SendTextMessageAsync(
                    chatId: e.Message.Chat,
                    text: "What exactly are you interested in?",
                    parseMode: ParseMode.Markdown,
                    disableNotification: true,
                    replyMarkup: Keyboards.SettingsBlock.OrderInformationKeyboard.FirstStep);
                    break;
                case "📌 Support":
                    await Properties.Config.client.SendTextMessageAsync(
                    chatId: e.Message.Chat,
                    text: "If you have any problem, please connect us @PorcelainBib",
                    parseMode: ParseMode.Markdown,
                    disableNotification: true,
                    replyMarkup: Keyboards.SettingsBlock.SettingsReplyKeyboard.BaseKeyboard);
                    break;
                case "⚠️ Back to main menu":
                    await Properties.Config.client.SendTextMessageAsync(
                    chatId: e.Message.Chat,
                    text: "You are in main menu!",
                    parseMode: ParseMode.Markdown,
                    disableNotification: true,
                    replyMarkup: Keyboards.BaseReplyKeyboard.BaseKeyboard);
                    break;
                case "/start":

                    break;
                case "/update":
                    await Properties.Config.client.SendTextMessageAsync(
                    chatId: e.Message.Chat,
                    text: $"Application was updated!",
                    parseMode: ParseMode.Markdown,
                    disableNotification: true,
                    replyMarkup: Keyboards.BaseReplyKeyboard.BaseKeyboard);
                    break;
            }
            #region CreateOrder
            #region Coins
            if (e.Message.ReplyToMessage != null && e.Message.ReplyToMessage.Text.Contains("💷 Enter yor cryptocurrency pair:"))
            {
                NewOrder.json_order.symbol = e.Message.Text;

                await Properties.Config.client.SendTextMessageAsync(
                chatId: e.Message.Chat,
                text: $"Specify the type of transaction (⬆️Buy / ⬇️Sell):",
                parseMode: ParseMode.Markdown,
                disableNotification: true,
                replyMarkup: Keyboards.NewOrderKeyboard.SecondStep);
            }
            #endregion
            #region Quantity
            if (e.Message.ReplyToMessage != null && e.Message.ReplyToMessage.Text.Contains("💰 Enter the amount:"))
            {
                await Properties.Config.client.DeleteMessageAsync(chatId: e.Message.Chat, e.Message.MessageId);

                try
                {
                    NewOrder.json_order.quantity = Convert.ToDecimal(e.Message.Text);
                    await NewOrder.Execute(e.Message.Chat.Id);
                }
                catch
                {
                    await Properties.Config.client.SendTextMessageAsync(
                    chatId: e.Message.Chat,
                    text: $"❌ Enter correct message!",
                    parseMode: ParseMode.Markdown,
                    disableNotification: true,
                    replyMarkup: Keyboards.BaseReplyKeyboard.BaseKeyboard);
                }  
            }
            #endregion
            #endregion
            #region DeleteOrder
            if (e.Message.ReplyToMessage != null && e.Message.ReplyToMessage.Text.Contains("Enter cryptocurrency pair for deleting:"))
            {

                DeleteOrder.order.symbol = e.Message.Text;

                await Properties.Config.client.SendTextMessageAsync(
                chatId: e.Message.Chat,
                text: $"Good, then enter order id:",
                parseMode: ParseMode.Markdown,
                disableNotification: true,
                replyMarkup: new ForceReplyMarkup() { Selective = true });
            }
            if (e.Message.ReplyToMessage != null && e.Message.ReplyToMessage.Text.Contains("Good, then enter order id:"))
            {
                try
                {
                    DeleteOrder.order.orderId = Convert.ToInt32(e.Message.Text);

                    await Properties.Config.client.SendTextMessageAsync(
                    chatId: e.Message.Chat,
                    text: $"Ok, order sent for deletion!",
                    parseMode: ParseMode.Markdown,
                    disableNotification: true,
                    replyMarkup: Keyboards.BaseReplyKeyboard.BaseKeyboard);

                    await DeleteOrder.Execute(e);
                }
                catch
                {
                    await Properties.Config.client.SendTextMessageAsync(
                    chatId: e.Message.Chat,
                    text: $"❌ Enter correct message!",
                    parseMode: ParseMode.Markdown,
                    disableNotification: true,
                    replyMarkup: Keyboards.BaseReplyKeyboard.BaseKeyboard);
                }
                
            }
            #endregion
            #region CurrentRate
            if (e.Message.ReplyToMessage != null && e.Message.ReplyToMessage.Text.Contains("Enter the cryptocurrency pair to get the current rate on popular exchanges:")) 
            {
                Commands.CurrentRateCommand commandRate = new Commands.CurrentRateCommand();
                await commandRate.Execute(e, e.Message.Text);
            }
            #endregion
            #region DailyStatistics
            if (e.Message.ReplyToMessage != null && e.Message.ReplyToMessage.Text.Contains("📤 Enter yor cryptocurrency coin:")) //или текст, который вы отправляли
            {
                Commands.DailyStatisticsCommand commandDaily = new Commands.DailyStatisticsCommand(e);
                await commandDaily.Execute(e, e.Message.Text);
            }
            #endregion
            #region AllOrders
            if(e.Message.ReplyToMessage != null && e.Message.ReplyToMessage.Text.Contains("Enter symbol for get all orders:")) //или текст, который вы отправляли
            {
                AllOrderInformation = new Commands.SettingsBlock.AllOrderInformationCommand(e);
                await AllOrderInformation.Execute(e);
            }
            #endregion
        }
    }
}
