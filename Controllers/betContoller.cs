using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UserService.Models;
using System.Net.Http;
using Newtonsoft.Json;

namespace UserService.Controllers {
    
    [Route("api/[controller]")]
    public class betController : Controller 
    {
        // check if bet exists
        [HttpGet]
        [Route("getBet/{betID}")]
        public async Task<Bet> getBet(string betId) 
        {
            var hc = Helpers.CouchDBConnect.GetClient("bets");
            var response = await hc.GetAsync("/bets/"+betId);
            if (!response.IsSuccessStatusCode)
                return null;
            HttpContent content = response.Content;
            string betString;
            Bet bet = null;
            betString = await content.ReadAsStringAsync();
            if (betString.Contains("_id"))
            {
                bet = JsonConvert.DeserializeObject<Bet>(betString);
                return bet;
            }
            else 
            {
                Console.WriteLine("can't deserialize bet");
                return null;
            }
        }

        // add bet to db
        [HttpPost]
        [Route("createBet")]
        public async Task<int> postBet([FromBody]creatingBet b)
        {
            var hc = Helpers.CouchDBConnect.GetClient("bets");
            string json = JsonConvert.SerializeObject(b);
            Console.WriteLine("the bet is: " + json);
            HttpContent htc = new StringContent(json,System.Text.Encoding.UTF8,"application/json");
            var response = await hc.PostAsync("",htc);
            if(response.IsSuccessStatusCode)
                return 1;
            else 
                Console.WriteLine("failed to get bet");
            return -1;
        }

        // update exsisting bet
        [HttpPost]
        [Route("updateBet")]
        public async Task<int> updateBet([FromBody]Bet b) 
        {            
            var hc = Helpers.CouchDBConnect.GetClient("bets");
            string json = JsonConvert.SerializeObject(b);
            HttpContent htc = new StringContent(json,System.Text.Encoding.UTF8,"application/json");
            var response = await hc.PutAsync("/bets/"+b._id,htc);
            if(response.IsSuccessStatusCode)
                return 1;
            else
                Console.WriteLine("bet didnt updated");
            return -1;
        }

        // delete bets
        [HttpPost]
        [Route("deleteBet")]
        public async Task<int> deleteBet([FromBody]Bet b)
        {
            var hc = Helpers.CouchDBConnect.GetClient("bets");
            string json = JsonConvert.SerializeObject(b);
            HttpContent htc = new StringContent(json,System.Text.Encoding.UTF8,"application/json");
            var response = await hc.DeleteAsync("/bets/"+b._id+"?rev="+b._rev);
            if(response.IsSuccessStatusCode)
            {
                Console.WriteLine("bet deleted");
                return 1;
            }
            else Console.WriteLine("bet not deleted");
            return -1;
        }
    }
}