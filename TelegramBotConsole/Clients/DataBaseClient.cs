using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Args;

namespace TelegramBotConsole.Clients
{
    static class DataBaseClient
    {
        public static async Task<Models.DataBaseBlock.DatabaseModel> GetData(string username)
        {
            HttpClient client = new HttpClient();

            var response = await client.GetAsync($"https://localhost:44370/api/DataBase?username=" + username);

            var content = response.Content.ReadAsStringAsync().Result;

            var json_response = JsonConvert.DeserializeObject<Models.DataBaseBlock.DatabaseModel>(content);

            return json_response;
        }
    }
}
