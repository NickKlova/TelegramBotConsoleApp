using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;
using System.Linq;

namespace TelegramBotConsole.Commands
{
    class AccountCommand : Command
    {
        public AccountCommand(MessageEventArgs e)
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(Properties.Config.BaseURL);

            var pass = Clients.DataBaseClient.GetData(e.Message.From.Username);

            client.DefaultRequestHeaders.Add("ApiKEY", pass.Result.ApiKey);
            client.DefaultRequestHeaders.Add("SecretKEY", pass.Result.SecretKey);
        }

        private async Task<Models.AccountModel> Request()
        {
            var response = await client.GetAsync($"api/Account/info");

            var content = response.Content.ReadAsStringAsync().Result;

            var json_response = JsonConvert.DeserializeObject<Models.AccountModel>(content);

            return json_response;
        }

        public async Task Execute(MessageEventArgs e)
        {
            var obj = await Request();

            string buf = $"📍 Account type: {obj.accountType}\n";
            for (int i = 0; i < obj.balances.Length; i++)
            {
                if (obj.balances[i].free > 0 || obj.balances[i].locked > 0)
                {
                    buf += $"\n💳 Coin: {obj.balances[i].asset}" +
                    $"\n Available balance: {obj.balances[i].free}" +
                    $"\n Locked balance: {obj.balances[i].locked}\n\n";
                }
            }

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
