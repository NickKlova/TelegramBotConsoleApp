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
    class NewOrderCommand : Command
    {
        public RequestBodyJson.NewOrderRequest json_order = new RequestBodyJson.NewOrderRequest();

        public NewOrderCommand(MessageEventArgs e)
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(Properties.Config.BaseURL);

            var pass = Clients.DataBaseClient.GetData(e.Message.From.Username);

            client.DefaultRequestHeaders.Add("ApiKEY", pass.Result.ApiKey);
            client.DefaultRequestHeaders.Add("SecretKEY", pass.Result.SecretKey);
        }

        private async Task<Models.NewOrderModel> Request()
        {
            var json = JsonConvert.SerializeObject(json_order);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"api/Order/create", data);

            if (response.IsSuccessStatusCode)
            {
                var content = response.Content.ReadAsStringAsync().Result;

                var json_response = JsonConvert.DeserializeObject<Models.NewOrderModel>(content);

                return json_response;
            }
            else
            {
                return null;
            }
            
        }

        public async Task Execute(long chatId)
        {
            var obj = await Request();

            if (obj == null)
            {
                await Properties.Config.client.SendTextMessageAsync(
                            chatId: chatId,
                            text: $"An error has occurred! ⚠️\n" +
                            $"Check the correctness of the data entered!",
                            parseMode: ParseMode.Markdown,
                            disableNotification: true,
                            replyMarkup: Keyboards.BaseReplyKeyboard.BaseKeyboard);
            }
            else
            {
                await Properties.Config.client.SendTextMessageAsync(
                            chatId: chatId,
                            text: $"🖇 Success! The order has been created.\n\n" +
                            $"⚠️ Client orderId: {obj.clientOrderId}\n" +
                            $"⚠️ OrderId: {obj.orderId}\n\n" +
                            $"📌 Symbol: {obj.symbol}\n\n" +
                            $"Price: {obj.price}\n\n" +
                            $"Purchased or sold: {obj.side}",
                            parseMode: ParseMode.Markdown,
                            disableNotification: true,
                            replyMarkup: Keyboards.BaseReplyKeyboard.BaseKeyboard);
            }
        }
    }
}
