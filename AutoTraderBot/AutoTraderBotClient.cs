using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AutoTraderBot
{
    class AutoTraderBotClient
    {
        private string _pair;
        private decimal _money;
        HttpClient _client;
        private Models.NewOrderRequest _json_order;
        public AutoTraderBotClient(string pair, decimal money)
        {
            _pair = pair;
            _money = money;
            _client = new HttpClient();
            _client.DefaultRequestHeaders.Add("AKEY", "ewiXtk9THv9Hmu9SdB6GDuJOTYnXwVm12VmCDCGrq0jBEXHGjFL17sxUCcoJmxTQ");
            _client.DefaultRequestHeaders.Add("SKEY", "rUHdC4m4pgvTJkrmCZCww0VWXk6ACjhRmt55wMDoR6nLzgmTqmNPefUO45Ew4Yyu");
        }

        public async Task Process()
        {
            List<Models.NewOrderResponse> orders = new List<Models.NewOrderResponse>();

            var info = RequestGetInfo();// получаю цену крипты за день + получаю текущую цену 
            var averange_price = (info.Result.highPrice+info.Result.lowPrice)/ 2;
            var current_price = info.Result.lastPrice;
           if(orders.Count < 2)
            {
                _json_order.quantity = _money;
                _json_order.side = "BUY";
                _json_order.symbol = _pair;

                var response = await RequestNewOrder();
                response.buyedprice = RequestGetInfo().Result.lastPrice;
                orders.Add(response);
            }
            else
            {
                var lastPrice = RequestGetInfo().Result.lastPrice;
                foreach (var order in orders)
                {
                    if((lastPrice - order.buyedprice)/order.buyedprice == 0.01)
                    {
                        _json_order.quantity = _money;
                        _json_order.side = "SELL";
                        _json_order.symbol = _pair;
                    }
                }
            }
               
            


        }
        private async Task<Models.GetDailyRateModel> RequestGetInfo()
        {
            var response = await _client.GetAsync("https://localhost:44393/api/GenInfo?symbol="+_pair);

            var content = response.Content.ReadAsStringAsync().Result;

            var json_response = JsonConvert.DeserializeObject<Models.GetDailyRateModel>(content);

            return json_response;
        }
        private async Task<Models.NewOrderResponse> RequestNewOrder()
        {
            var json = JsonConvert.SerializeObject(_json_order);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync($"https://localhost:44393/api/Order/createOrder", data);

            var content = response.Content.ReadAsStringAsync().Result;

            var json_response = JsonConvert.DeserializeObject<Models.NewOrderResponse>(content);

            return json_response;
        }
    }
}
