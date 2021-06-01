using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;

namespace TelegramBotConsole.Commands.SettingsBlock
{
    class AllOrderInformationCommand : Command
    {
        public AllOrderInformationCommand(MessageEventArgs e)
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(Properties.Config.BaseURL);

            var pass = Clients.DataBaseClient.GetData(e.Message.From.Username);

            client.DefaultRequestHeaders.Add("ApiKEY", pass.Result.ApiKey);
            client.DefaultRequestHeaders.Add("SecretKEY", pass.Result.SecretKey);
        }

        private async Task<Models.SettingsBlock.OrderInformationModel[]> Request(string symbol)
        {
            var response = await client.GetAsync($"api/Order/getinfo/all?symbol={symbol}");
            if (response.IsSuccessStatusCode)
            {
                var content = response.Content.ReadAsStringAsync().Result;

            var json_response = JsonConvert.DeserializeObject<Models.SettingsBlock.OrderInformationModel[]>(content);

            return json_response;
            }
            else
            {
                return null;
            }
        }

        public async Task Execute(MessageEventArgs e)
        {
            var response = await Request(e.Message.Text);
            if (response != null)
            {
                string buf = $"📱 Information about order:\n\n";
                foreach (var i in response)
                {
                    buf += $"CryptoPair: {i.symbol}\n" +
                           $"Status: {i.status}\n" +
                           $"Type: {i.type}\n" +
                           $"Buy | sell order: {i.side}\n" +
                           $"Price: {i.price}$\n\n";
                }
                await Properties.Config.client.SendTextMessageAsync(
                               chatId: e.Message.Chat,
                           text: buf,
                           parseMode: ParseMode.Markdown,
                           disableNotification: true,
                           replyToMessageId: e.Message.MessageId,
                           replyMarkup: Keyboards.SettingsBlock.SettingsReplyKeyboard.BaseKeyboard);
            }
            else
            {
                await Properties.Config.client.SendTextMessageAsync(
                               chatId: e.Message.Chat,
                           text: $"Enter correct message!",
                           parseMode: ParseMode.Markdown,
                           disableNotification: true,
                           replyToMessageId: e.Message.MessageId,
                           replyMarkup: Keyboards.SettingsBlock.SettingsReplyKeyboard.BaseKeyboard);
            }
            
        }
    }
}
