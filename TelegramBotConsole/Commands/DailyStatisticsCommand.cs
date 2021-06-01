using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;

namespace TelegramBotConsole.Commands
{
    class DailyStatisticsCommand : Command
    {
        public DailyStatisticsCommand(MessageEventArgs e)
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(Properties.Config.BaseURL);

            var pass = Clients.DataBaseClient.GetData(e.Message.From.Username);

            client.DefaultRequestHeaders.Add("ApiKEY", pass.Result.ApiKey);
        }

        private async Task<Models.DailyStatisticsModel> Request(string symbol = "BTCUSDT")
        {
            var response = await client.GetAsync("api/Rate?symbol=" + symbol);
            if (response.IsSuccessStatusCode)
            {
                var content = response.Content.ReadAsStringAsync().Result;

                var json_response = JsonConvert.DeserializeObject<Models.DailyStatisticsModel>(content);

                return json_response;
            }
            else
            {
                return null;
            }
        }

        public async Task Execute(MessageEventArgs e, string symbol = null)
        {
            if(symbol == null)
            {
                var response = await Request();

                string buf = $"🧳 CryptoPair: {response.symbol}\n\n" +
                        $"Highest price per day: {response.highPrice}$\n\n" +
                        $"Lowest price per day: {response.lowPrice}$\n\n" +
                        $"Current price: {response.lastPrice}$\n\n" +
                        $"How much has the price changed: {response.priceChange}$\n\n" +
                        $"Market volatility: {response.volume}$";

                await Properties.Config.client.SendTextMessageAsync(
                chatId: e.Message.Chat,
            text: buf,
            parseMode: ParseMode.Markdown,
            disableNotification: true,
            replyToMessageId: e.Message.MessageId,
            replyMarkup: Keyboards.DailyStatisticsKeyboard.FirstStep);
            }
            else
            {
                var response = await Request(symbol);
                if(response != null)
                {
                    string buf = $"🧳 CryptoPair: {response.symbol}\n\n" +
                        $"Highest price per day: {response.highPrice}$\n\n" +
                        $"Lowest price per day: {response.lowPrice}$\n\n" +
                        $"Current price: {response.lastPrice}$\n\n" +
                        $"How much has the price changed: {response.priceChange}$\n\n" +
                        $"Market volatility: {response.volume}$";

                    await Properties.Config.client.SendTextMessageAsync(
                    chatId: e.Message.Chat,
                text: buf,
                parseMode: ParseMode.Markdown,
                disableNotification: true,
                replyToMessageId: e.Message.MessageId,
                replyMarkup: Keyboards.BaseReplyKeyboard.BaseKeyboard);
                }
                else
                {
                    await Properties.Config.client.SendTextMessageAsync(
                    chatId: e.Message.Chat,
                text: "Enter correct symbol! 😡",
                parseMode: ParseMode.Markdown,
                disableNotification: true,
                replyToMessageId: e.Message.MessageId,
                replyMarkup: Keyboards.BaseReplyKeyboard.BaseKeyboard);
                }
            }
        }
    }
}
