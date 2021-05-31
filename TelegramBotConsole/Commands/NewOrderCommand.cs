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

        public NewOrderCommand()
        {
            client = new HttpClient();
            client.DefaultRequestHeaders.Add("AKEY", "ewiXtk9THv9Hmu9SdB6GDuJOTYnXwVm12VmCDCGrq0jBEXHGjFL17sxUCcoJmxTQ");
            client.DefaultRequestHeaders.Add("SKEY", "rUHdC4m4pgvTJkrmCZCww0VWXk6ACjhRmt55wMDoR6nLzgmTqmNPefUO45Ew4Yyu");
        }

        public async Task<Models.NewOrderModel> Request()
        {
            var json = JsonConvert.SerializeObject(json_order);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"https://localhost:44393/api/Order/createOrder", data);

            var content = response.Content.ReadAsStringAsync().Result;

            var json_response = JsonConvert.DeserializeObject<Models.NewOrderModel>(content);

            return json_response;
        }

        public async Task Execute(long ChatId)
        {
            var obj = await Request();

            await Properties.Config.client.SendTextMessageAsync(
            chatId: ChatId,
            text: $"The order has been sent :)",
            parseMode: ParseMode.Markdown,
            disableNotification: true,
            replyMarkup: Keyboards.BaseReplyKeyboard.BaseKeyboard);
        }
    }
}
