using AutomataServer.Models.recordsmodels;
using Newtonsoft.Json;


namespace AutomataServer.Models.Services
{
    public class LowMarket_Prices : BaseHttpToSqlResponse
    {
        public List<recordsmodels.User>? records { get; set; }

        public static async Task<LowMarket_Prices>  GetAllPrices() 
        {
            using (HttpClient client = new HttpClient())
            {
                string result = await sqlQuery($"select *  from LowMarketPrices");

                LowMarket_Prices data = JsonConvert.DeserializeObject<LowMarket_Prices>(result);

                return data.error.has == true ? null : data;
            }
            


        }

        

        public static async Task<LowMarket_Prices> GetPrice(string resource)
        {
            
            using (HttpClient client = new HttpClient())
            {
                string result = await sqlQuery($"select *  from LowMarketPrices where ResourceName='{resource}'");
                
                LowMarket_Prices data = JsonConvert.DeserializeObject<LowMarket_Prices>(result);

                return data.error.has == true ? null : data;
            }
            

        }

    }
}
