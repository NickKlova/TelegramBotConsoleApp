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
    class CurrentRateCommand : Command
    {
        public CurrentRateCommand()
        {
            client = new HttpClient();
        }

        private async Task<Models.ExchangeInfoModel> Request(string symbol)
        {
            var response = await client.GetAsync(Properties.Config.BaseURL + "api/ExchangePrice?symbol=" + symbol);

            if (response.IsSuccessStatusCode)
            {
                var content = response.Content.ReadAsStringAsync().Result;

                var json_response = JsonConvert.DeserializeObject<Models.ExchangeInfoModel>(content);

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
                var response_BTC = await Request("bitcoin");
                var response_ETH = await Request("ethereum");
                var response_XRP = await Request("xrp");

                string buf = null;
                buf += Properties.Tools.GetCurrentRate(response_BTC, "bitcoin", "✅ BITCOIN");
                buf += Properties.Tools.GetCurrentRate(response_ETH, "ethereum", "💹 ETHEREUM");
                buf += Properties.Tools.GetCurrentRate(response_XRP, "xrp", "❎ RIPPLE");

                await Properties.Config.client.SendTextMessageAsync(
                chatId: e.Message.Chat,
                text: buf,
                parseMode: ParseMode.Markdown,
                disableNotification: true,
                replyToMessageId: e.Message.MessageId,
                replyMarkup: Keyboards.CurrentRateKeyboard.FirstStep);
            }
            else
            {
                var response = await Request(symbol);
                if (response != null)
                {
                    string buf = null;
                    buf += Properties.Tools.GetCurrentRate(response, symbol);
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
                    text: "Enter an existing coin! 😡",
                    parseMode: ParseMode.Markdown,
                    disableNotification: true,
                    replyToMessageId: e.Message.MessageId,
                    replyMarkup: Keyboards.BaseReplyKeyboard.BaseKeyboard);
                }
            }
        }
    }
}
