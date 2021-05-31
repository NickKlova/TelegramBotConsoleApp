using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace TelegramBotConsole.Commands
{
    abstract class Command
    {
        protected HttpClient client;
    }
}
