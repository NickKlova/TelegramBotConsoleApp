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
        Commands.SettingsBlock.OrderInformationCommand OrderInformation;
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
            if (e.CallbackQuery.Data == "LTCBTC")
            {
                Properties.Tools.CallbackQueryCoins(e, NewOrder, "LTCBTC", "You choose LTC to BTC!");
            }

            if (e.CallbackQuery.Data == "coinCustom")
            {
                var message = await Properties.Config.client.SendTextMessageAsync(
                chatId: e.CallbackQuery.Message.Chat,
                text: $"Enter yor cryptocurrency pair:",
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
            if (e.CallbackQuery.Data == "100percent")
            {
                Properties.Tools.CallbackQueryQuantity(e, NewOrder, 100, "100%? You're tough!");
            }
            if (e.CallbackQuery.Data == "75percent")
            {
                Properties.Tools.CallbackQueryQuantity(e, NewOrder, 75, "75%? Right choise!");
            }
            if (e.CallbackQuery.Data == "50percent")
            {
                Properties.Tools.CallbackQueryQuantity(e, NewOrder, 50, "50%? Not bad!");
            }
            if (e.CallbackQuery.Data == "25percent")
            {
                Properties.Tools.CallbackQueryQuantity(e, NewOrder, 25, "25%? Pretty boy!");
            }
            if (e.CallbackQuery.Data == "customPercent")
            {
                await Properties.Config.client.DeleteMessageAsync(chatId: e.CallbackQuery.Message.Chat, e.CallbackQuery.Message.MessageId);

                var message = await Properties.Config.client.SendTextMessageAsync(
                chatId: e.CallbackQuery.Message.Chat,
                text: $"Enter the amount:",
                parseMode: ParseMode.Markdown,
                disableNotification: true,
                replyMarkup: new ForceReplyMarkup() { Selective = true });
            }
            #endregion
            #region ExchangeInfo
            if (e.CallbackQuery.Data == "AnotherCoinCurrentRate")
            {
                var message = await Properties.Config.client.SendTextMessageAsync(
                                chatId: e.CallbackQuery.Message.Chat,
                                text: $"Enter yor cryptocurrency pair for exchangeinfo:",
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
                                text: $"Enter yor cryptocurrency coin:",
                                parseMode: ParseMode.Markdown,
                                disableNotification: true,
                                replyMarkup: new ForceReplyMarkup() { Selective = true });
            }
            #endregion
            #region SettingsBlock
            if(e.CallbackQuery.Data == "SpecificOrder")
            {
                var message = await Properties.Config.client.SendTextMessageAsync(
                                chatId: e.CallbackQuery.Message.Chat,
                                text: $"Enter symbol:",
                                parseMode: ParseMode.Markdown,
                                disableNotification: true,
                                replyMarkup: new ForceReplyMarkup() { Selective = true });
            }
            if(e.CallbackQuery.Data == "AllOpenOrders")
            {
                Commands.SettingsBlock.AllOpenOrderInformationCommand command = new Commands.SettingsBlock.AllOpenOrderInformationCommand(e);
                await command.Execute(e);
            }
            if (e.CallbackQuery.Data == "OpenOrders")
            {
                var message = await Properties.Config.client.SendTextMessageAsync(
                               chatId: e.CallbackQuery.Message.Chat,
                               text: $"Enter symbol for orders:",
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
                    text: $"Choose cryptocurrency pair: ",
                    parseMode: ParseMode.Markdown,
                    disableNotification: true,
                    replyMarkup: Keyboards.NewOrderKeyboard.FirstStep);
                    break;
                case "❌ Delete order":
                    DeleteOrder = new Commands.DeleteOrderCommand(e);
                    await Properties.Config.client.SendTextMessageAsync(
                    chatId: e.Message.Chat,
                    text: $"Enter cryptocurrency pair for deleting: ",
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
                    OrderInformation = new Commands.SettingsBlock.OrderInformationCommand(e);
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
            #region Coins
            if (e.Message.ReplyToMessage != null && e.Message.ReplyToMessage.Text.Contains("Enter yor cryptocurrency pair:")) //или текст, который вы отправляли
            {
                NewOrder.json_order.symbol = e.Message.Text;

                await Properties.Config.client.SendTextMessageAsync(
                chatId: e.Message.Chat,
                text: $"Specify the type of transaction (Buy/Sell): ",
                parseMode: ParseMode.Markdown,
                disableNotification: true,
                replyMarkup: Keyboards.NewOrderKeyboard.SecondStep);
            }
            #endregion
            #region Quantity
            if (e.Message.ReplyToMessage != null && e.Message.ReplyToMessage.Text.Contains("Enter the amount:")) //или текст, который вы отправляли
            {
                NewOrder.json_order.quantity = Convert.ToDecimal(e.Message.Text);

                await NewOrder.Execute(e.Message.Chat.Id);
            }
            #endregion
            #region DeleteOrder
            if (e.Message.ReplyToMessage != null && e.Message.ReplyToMessage.Text.Contains("Enter cryptocurrency pair for deleting:")) //или текст, который вы отправляли
            {
                DeleteOrder.order.symbol = e.Message.Text;

                await Properties.Config.client.SendTextMessageAsync(
                chatId: e.Message.Chat,
                text: $"Ok, then enter order id: ",
                parseMode: ParseMode.Markdown,
                disableNotification: true,
                replyMarkup: new ForceReplyMarkup() { Selective = true });
            }
            if (e.Message.ReplyToMessage != null && e.Message.ReplyToMessage.Text.Contains("Ok, then enter order id:")) //или текст, который вы отправляли
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
            #endregion
            #region ExchangeInfo
            if (e.Message.ReplyToMessage != null && e.Message.ReplyToMessage.Text.Contains("Enter yor cryptocurrency pair for exchangeinfo:")) //или текст, который вы отправляли
            {
                Commands.CurrentRateCommand commandRate = new Commands.CurrentRateCommand();
                await commandRate.Execute(e, e.Message.Text);
            }
            #endregion
            #region DailyStatistics
            if (e.Message.ReplyToMessage != null && e.Message.ReplyToMessage.Text.Contains("Enter yor cryptocurrency coin:")) //или текст, который вы отправляли
            {
                Commands.DailyStatisticsCommand commandDaily = new Commands.DailyStatisticsCommand(e);
                await commandDaily.Execute(e, e.Message.Text);
            }
            #endregion
            #region SettingsBlock
            #region OrderInfo
            if (e.Message.ReplyToMessage != null && e.Message.ReplyToMessage.Text.Contains("Enter symbol:")) //или текст, который вы отправляли
            {
                OrderInformation.orderinfo.symbol = e.Message.Text;

                var message = await Properties.Config.client.SendTextMessageAsync(
                                chatId: e.Message.Chat,
                                text: $"Enter order Id:",
                                parseMode: ParseMode.Markdown,
                                disableNotification: true,
                                replyMarkup: new ForceReplyMarkup() { Selective = true });

            }
            if (e.Message.ReplyToMessage != null && e.Message.ReplyToMessage.Text.Contains("Enter order Id:")) //или текст, который вы отправляли
            {
                //OrderInformation.orderinfo.orderId = Convert.ToInt32(e.Message.Text);
                Commands.SettingsBlock.OrderInformationCommand OrderInfo = new Commands.SettingsBlock.OrderInformationCommand(e);
                await OrderInfo.Execute(e);
            }
            #endregion
            #region AllOrders
            if(e.Message.ReplyToMessage != null && e.Message.ReplyToMessage.Text.Contains("Enter symbol for orders:")) //или текст, который вы отправляли
            {
                Commands.SettingsBlock.AllOrderInformationCommand command = new Commands.SettingsBlock.AllOrderInformationCommand(e);
                await command.Execute(e);
            }
            #endregion
            #endregion
        }
    }
}
