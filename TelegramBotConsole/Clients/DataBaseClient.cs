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
        public static async Task PostData(Models.DataBaseBlock.DatabaseModel db)
        {
            HttpClient client = new HttpClient();

            var json = JsonConvert.SerializeObject(db);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"https://localhost:44370/api/DataBase", data);
        }
    }
}
