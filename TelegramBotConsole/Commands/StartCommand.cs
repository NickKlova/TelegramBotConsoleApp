using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;

namespace TelegramBotConsole.Commands
{
    class StartCommand : Command
    {
        public Models.DataBaseBlock.DatabaseModel db = new Models.DataBaseBlock.DatabaseModel();
        MessageEventArgs _e;
        public StartCommand(MessageEventArgs e)
        {
            _e = e;
            client = new HttpClient();
            db.Username = e.Message.From.Username;
            client.BaseAddress = new Uri(Properties.Config.BaseURL);
        }
        public async Task Execute()
        {
            await Clients.DataBaseClient.PostData(db);

            await Properties.Config.client.SendTextMessageAsync(
                chatId: _e.Message.Chat,
                text: "Enjoy your use!",
                parseMode: ParseMode.Markdown,
                disableNotification: true,
                replyToMessageId: _e.Message.MessageId,
                replyMarkup: Keyboards.BaseReplyKeyboard.BaseKeyboard);
        }
    }
}
