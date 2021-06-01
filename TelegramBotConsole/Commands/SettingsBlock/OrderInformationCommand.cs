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
    class OrderInformationCommand : Command
    {
        public RequestBodyJson.OrderInformationRequest orderinfo = new RequestBodyJson.OrderInformationRequest();
        public OrderInformationCommand(MessageEventArgs e)
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(Properties.Config.BaseURL);

            var pass = Clients.DataBaseClient.GetData(e.Message.From.Username);

            client.DefaultRequestHeaders.Add("ApiKEY", pass.Result.ApiKey);
            client.DefaultRequestHeaders.Add("SecretKEY", pass.Result.SecretKey);
        }

        private async Task<Models.SettingsBlock.OrderInformationModel> Request()
        {
            var response = await client.GetAsync($"api/Order/getinfo?symbol={orderinfo.symbol}&orderId={orderinfo.orderId}");

            var content = response.Content.ReadAsStringAsync().Result;

            var json_response = JsonConvert.DeserializeObject<Models.SettingsBlock.OrderInformationModel>(content);

            return json_response;
        }

        public async Task Execute(MessageEventArgs e)
        {
            var response = Request();

            await Properties.Config.client.SendTextMessageAsync(
                chatId: e.Message.Chat,
            text: $"Information about order",
            parseMode: ParseMode.Markdown,
            disableNotification: true,
            replyToMessageId: e.Message.MessageId,
            replyMarkup: Keyboards.SettingsBlock.SettingsReplyKeyboard.BaseKeyboard);
        }
    }
}
