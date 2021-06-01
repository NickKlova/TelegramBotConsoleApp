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
    class AllOpenOrderInformationCommand : Command
    {
        public AllOpenOrderInformationCommand(CallbackQueryEventArgs e)
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(Properties.Config.BaseURL);

            var pass = Clients.DataBaseClient.GetData(e.CallbackQuery.Message.From.Username);

            client.DefaultRequestHeaders.Add("ApiKEY", pass.Result.ApiKey);
            client.DefaultRequestHeaders.Add("SecretKEY", pass.Result.SecretKey);
        }

        private async Task<Models.SettingsBlock.OrderInformationModel[]> Request()
        {
            var response = await client.GetAsync($"api/Order/getinfo/all/open");

            var content = response.Content.ReadAsStringAsync().Result;

            var json_response = JsonConvert.DeserializeObject<Models.SettingsBlock.OrderInformationModel[]>(content);

            return json_response;
        }

        public async Task Execute(CallbackQueryEventArgs e)
        {
            var response = Request();

            await Properties.Config.client.SendTextMessageAsync(
                chatId: e.CallbackQuery.Message.Chat,
            text: $"Information about order",
            parseMode: ParseMode.Markdown,
            disableNotification: true,
            replyToMessageId: e.CallbackQuery.Message.MessageId,
            replyMarkup: Keyboards.SettingsBlock.SettingsReplyKeyboard.BaseKeyboard);
        }
    }
}
