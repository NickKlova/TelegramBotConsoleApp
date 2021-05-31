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
        public AllOpenOrderInformationCommand()
        {
            client = new HttpClient();
            client.DefaultRequestHeaders.Add("AKEY", "ewiXtk9THv9Hmu9SdB6GDuJOTYnXwVm12VmCDCGrq0jBEXHGjFL17sxUCcoJmxTQ");
            client.DefaultRequestHeaders.Add("SKEY", "rUHdC4m4pgvTJkrmCZCww0VWXk6ACjhRmt55wMDoR6nLzgmTqmNPefUO45Ew4Yyu");
        }

        private async Task<Models.SettingsBlock.OrderInformationModel[]> Request()
        {
            var response = await client.GetAsync($"https://localhost:44393/api/Order/info/getall/open");

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
