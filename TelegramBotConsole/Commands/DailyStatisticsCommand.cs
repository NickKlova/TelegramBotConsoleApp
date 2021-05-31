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
        public DailyStatisticsCommand()
        {
            client = new HttpClient();
            client.DefaultRequestHeaders.Add("AKEY", "ewiXtk9THv9Hmu9SdB6GDuJOTYnXwVm12VmCDCGrq0jBEXHGjFL17sxUCcoJmxTQ");
        }

        private async Task<Models.DailyStatisticsModel> Request(string symbol = "BTCUSDT")
        {
            var response = await client.GetAsync("https://localhost:44393/api/GenInfo?symbol="+symbol);

            var content = response.Content.ReadAsStringAsync().Result;

            var json_response = JsonConvert.DeserializeObject<Models.DailyStatisticsModel>(content);

            return json_response;
        }

        public async Task Execute(MessageEventArgs e, string symbol = null)
        {
            if(symbol == null)
            {
                var response = await Request();

                string buf = $"{response.highPrice}, {response.lowPrice}, {response.lastPrice}, {response.priceChange}, {response.volume}, {response.symbol}";

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

                string buf = $"{response.highPrice}, {response.lowPrice}, {response.lastPrice}, {response.priceChange}, {response.volume}, {response.symbol}";

                await Properties.Config.client.SendTextMessageAsync(
                chatId: e.Message.Chat,
            text: buf,
            parseMode: ParseMode.Markdown,
            disableNotification: true,
            replyToMessageId: e.Message.MessageId,
            replyMarkup: Keyboards.BaseReplyKeyboard.BaseKeyboard);
            }
        }
    }
}
