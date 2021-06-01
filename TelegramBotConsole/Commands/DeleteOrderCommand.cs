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
    class DeleteOrderCommand : Command
    {
        public RequestBodyJson.DeleteOrderRequest order = new RequestBodyJson.DeleteOrderRequest();
        public DeleteOrderCommand(MessageEventArgs e)
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(Properties.Config.BaseURL);

            var pass = Clients.DataBaseClient.GetData(e.Message.From.Username);

            client.DefaultRequestHeaders.Add("ApiKEY", pass.Result.ApiKey);
            client.DefaultRequestHeaders.Add("SecretKEY", pass.Result.SecretKey);
        }

        private async Task<Models.DeleteOrderModel> Request()
        {
            var response = await client.DeleteAsync($"api/Order/cancel?symbol={order.symbol}&orderId={order.orderId}");
            if (response.IsSuccessStatusCode)
            {
                var content = response.Content.ReadAsStringAsync().Result;

                var json_response = JsonConvert.DeserializeObject<Models.DeleteOrderModel>(content);

                return json_response;
            }
            else
            {
                return null;
            }    
        }

        public async Task Execute(MessageEventArgs e)
        {
            var obj = Request().Result;
            if(obj != null)
            {
                await Properties.Config.client.SendTextMessageAsync(
            chatId: e.Message.Chat,
            text: $"Coin: {obj.symbol}\n" +
            $"Id: {obj.orderId}\n" +
            $"Price: {obj.price}\n" +
            $"Status: {obj.status}",
            parseMode: ParseMode.Markdown,
            disableNotification: true,
            replyToMessageId: e.Message.MessageId,
            replyMarkup: Keyboards.BaseReplyKeyboard.BaseKeyboard);
            }
            else
            {
                await Properties.Config.client.SendTextMessageAsync(
                                            chatId: e.Message.Chat.Id,
                                            text: $"An error has occurred! ⚠️\n" +
                                            $"Check the correctness of the data entered!",
                                            parseMode: ParseMode.Markdown,
                                            disableNotification: true,
                                            replyMarkup: Keyboards.BaseReplyKeyboard.BaseKeyboard);
            }
        }
    }
}
