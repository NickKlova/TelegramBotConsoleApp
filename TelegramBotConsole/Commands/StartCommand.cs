using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Telegram.Bot.Args;

namespace TelegramBotConsole.Commands
{
    class StartCommand : Command
    {
        public StartCommand(MessageEventArgs e)
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(Properties.Config.BaseURL);
        }
    }
}
