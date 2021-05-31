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
        public AccountCommand()
        {
            client = new HttpClient();
            client.DefaultRequestHeaders.Add("AKEY", "ewiXtk9THv9Hmu9SdB6GDuJOTYnXwVm12VmCDCGrq0jBEXHGjFL17sxUCcoJmxTQ");
            client.DefaultRequestHeaders.Add("SKEY", "rUHdC4m4pgvTJkrmCZCww0VWXk6ACjhRmt55wMDoR6nLzgmTqmNPefUO45Ew4Yyu");
        }

        private async Task<Models.AccountModel> Request()
        {
            var response = await client.GetAsync($"https://localhost:44393/api/Account/info/get");

            var content = response.Content.ReadAsStringAsync().Result;

            var json_response = JsonConvert.DeserializeObject<Models.AccountModel>(content);

            return json_response;
        }

        public async Task Execute(MessageEventArgs e)
        {
            var account = new Commands.AccountCommand();
            Models.AccountModel obj = await account.Request();

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
