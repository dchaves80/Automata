using AutomataServer.Models;
using AutomataServer.Models.Services;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.Diagnostics.Eventing.Reader;

namespace AutomataServer.Controllers
{

    [ApiController]
    [Route("[controller]")]

    public class LowMarketController:ControllerBase
    {
       
        [HttpGet,Route("GetPrices")]

        public async Task<LowMarket_Prices> GetPrices() 
        {
            
            return await LowMarket_Prices.GetAllPrices();
        }

        [HttpGet, Route("GetPrice")]
        public async Task<LowMarket_Prices> GetPrices(string resource)
        {
            
            return await LowMarket_Prices.GetPrice(resource);
            
        }

        
        
    }
}
