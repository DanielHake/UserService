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
    public class playerController : Controller 
    {
        // check if player exists
        [HttpGet]
        [Route("getPlayer/{playerId}")]
        public async Task<Player> GetPlayer(string playerId) 
        {
            Player player = null;
            var hc = Helpers.CouchDBConnect.GetClient("players");
            var response = await hc.GetAsync("/players/"+playerId);
            if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine("player not found in db " + playerId);
                    return null;
                }
            HttpContent content = response.Content;
            string json =await content.ReadAsStringAsync();
            if (json.Contains("_id"))
                {
                    player = (Player) JsonConvert.DeserializeObject(json,typeof(Player));
                }
            else Console.WriteLine("player not found in db " + playerId);
            return player;
        }

        // add player to db
        [HttpPost]
        [Route("createPlayer")]
        public async Task<int> postPlayer([FromBody]creatingPlayer p)
        {
            var hc = Helpers.CouchDBConnect.GetClient("players");
            string json = JsonConvert.SerializeObject(p);
            HttpContent htc = new StringContent(json,System.Text.Encoding.UTF8,"application/json");
            var response = await hc.PostAsync("",htc);
            if(!response.IsSuccessStatusCode)
            {
                Console.WriteLine("player not created");
                return -1;
            }
            else
                Console.WriteLine("player Created");

            return 1;
        }

        // update exsisting player
        [HttpPost]
        [Route("updatePlayer")]
        public async Task<int> updatePlayer([FromBody]Player p) 
        {
            var hc = Helpers.CouchDBConnect.GetClient("players");
            string json = JsonConvert.SerializeObject(p);
            HttpContent htc = new StringContent(json,System.Text.Encoding.UTF8,"application/json");
            var response = await hc.PutAsync("/players/"+p._id,htc);
            if(!response.IsSuccessStatusCode)
            {
                Console.WriteLine("Player updated failed");
                return -1;
            }
            else Console.WriteLine("player updated");
            return 1;
        }

        // delete players
        [HttpPost]
        [Route("deletePlayer")]
        public async Task<int> deletePlayer([FromBody]Player b)
        {
            var hc = Helpers.CouchDBConnect.GetClient("players");
            string json = JsonConvert.SerializeObject(b);
            HttpContent htc = new StringContent(json,System.Text.Encoding.UTF8,"application/json");
            var response = await hc.DeleteAsync("/players/"+b._id+"?rev="+b._rev);
            if(response.IsSuccessStatusCode)
            {
                Console.WriteLine("player deleted");
                return 1;
            }
            else Console.WriteLine("Player not deleted");
            return 1;
        }
    }
}