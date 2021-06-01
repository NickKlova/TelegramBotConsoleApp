using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot.Args;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Telegram.Bot.Types.Enums;

namespace TelegramBotConsole.Commands.SettingsBlock
{
    class ChangeApiKeysCommand : Command
    {
        public Models.DataBaseBlock.DatabaseModel Model;
        MessageEventArgs _e;
        public ChangeApiKeysCommand(MessageEventArgs e)
        {
            _e = e;
            client = new HttpClient();
            Model = new Models.DataBaseBlock.DatabaseModel();
            Model.Username = e.Message.From.Username;
        }
        private async Task<bool> Request()
        {
            var json = JsonConvert.SerializeObject(Model);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PutAsync($"api/Order/create", data);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task Execute()
        {
            var request = await Request();

            if(request == true)
            {
                await Properties.Config.client.SendTextMessageAsync(
                            chatId: _e.Message.Chat.Id,
                            text: $"Keys have been updated",
                            parseMode: ParseMode.Markdown,
                            disableNotification: true,
                            replyMarkup: Keyboards.BaseReplyKeyboard.BaseKeyboard);
            }
            else
            {
                await Properties.Config.client.SendTextMessageAsync(
                            chatId: _e.Message.Chat.Id,
                            text: $"Failed.",
                            parseMode: ParseMode.Markdown,
                            disableNotification: true,
                            replyMarkup: Keyboards.BaseReplyKeyboard.BaseKeyboard);
            }
        }
    }
}
