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

            var content = response.Content.ReadAsStringAsync().Result;

            var json_response = JsonConvert.DeserializeObject<Models.ExchangeInfoModel>(content);

            return json_response;
        }

        public async Task Execute(MessageEventArgs e, string symbol = null)
        {
            if(symbol == null)
            {
                var response_BTC = await Request("bitcoin");
                var response_ETH = await Request("ethereum");
                var response_XRP = await Request("xrp");

                string buf = null;
                buf += Properties.Tools.GetCurrentRate("bitcoin", response_BTC);
                buf += Properties.Tools.GetCurrentRate("ethereum", response_ETH);
                buf += Properties.Tools.GetCurrentRate("xrp", response_XRP);

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

                string buf = null;
                buf += Properties.Tools.GetCurrentRate(symbol, response);
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
