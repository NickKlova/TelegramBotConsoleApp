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
        public DeleteOrderCommand()
        {
            client = new HttpClient();
            client.DefaultRequestHeaders.Add("AKEY", "ewiXtk9THv9Hmu9SdB6GDuJOTYnXwVm12VmCDCGrq0jBEXHGjFL17sxUCcoJmxTQ");
            client.DefaultRequestHeaders.Add("SKEY", "rUHdC4m4pgvTJkrmCZCww0VWXk6ACjhRmt55wMDoR6nLzgmTqmNPefUO45Ew4Yyu");
        }

        private async Task<Models.DeleteOrderModel> Request()
        {
            var response = await client.DeleteAsync($"https://localhost:44393/api/Order/cancelOrder?symbol={order.symbol}&orderId={order.orderId}");

            var content = response.Content.ReadAsStringAsync().Result;

            var json_response = JsonConvert.DeserializeObject<Models.DeleteOrderModel>(content);

            return json_response;
        }

        public async Task Execute(MessageEventArgs e)
        {
            var obj = Request().Result;

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
    }
}
