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
    public class gameController : Controller 
    {
        // check if game exists
        [HttpGet]
        [Route("getGame/{gameId}")]
        public async Task<Game> GetGame(string gameId) 
        {
            Game game = null;
            var hc = Helpers.CouchDBConnect.GetClient("games");
            HttpResponseMessage response = await hc.GetAsync("/games/"+gameId);
            if (response.IsSuccessStatusCode)
            {
                HttpContent content = response.Content;
                string json = await content.ReadAsStringAsync();
                if (!json.Contains("_id"))
                {
                    Console.WriteLine("game is empty " + gameId);
                    return null;
                }
                else 
                {
                    Console.WriteLine("Game json :" + json);
                    game = (Game) JsonConvert.DeserializeObject<Game>(json);
                    if (game != null)
                    {
                        return game;
                    }
                    else Console.WriteLine("game is null");
                }
            }
            Console.WriteLine("get game failed " + gameId);
            return game;
        }

        // add game to db
        [HttpPost]
        [Route("createGame")]
        public async Task<int> postGame([FromBody]creatingGame g)
        { 
            HttpClient hc = Helpers.CouchDBConnect.GetClient("games");
            string json = JsonConvert.SerializeObject(g);
            Console.WriteLine(json);
            HttpContent htc = new StringContent(json,System.Text.Encoding.UTF8,"application/json");
            HttpResponseMessage response = await hc.PostAsync("",htc);
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("game created");
                return 1;
            }
            Console.WriteLine("game creation falied");
            return -1;
        }

        // update exsisting game
        [HttpPost]
        [Route("updateGame")]
        public async Task<int> updateGame([FromBody]Game g) 
        {
            var hc = Helpers.CouchDBConnect.GetClient("games");
            string json = JsonConvert.SerializeObject(g);
            Console.WriteLine(json);
            HttpContent htc = new StringContent(json,System.Text.Encoding.UTF8,"application/json");
            var response = await hc.PutAsync("/games/"+g._id,htc);
               if(response.IsSuccessStatusCode)
                {
                    Console.WriteLine("game updated");
                    return 1;
                }
                else 
                    Console.WriteLine("game update failed");
                return -1;
        }

        // delete games 
        [HttpPost]
        [Route("deleteGame")]
        public async Task<int> deleteGame([FromBody]Game b)
        {
            var hc = Helpers.CouchDBConnect.GetClient("games");
            string json = JsonConvert.SerializeObject(b);
            HttpContent htc = new StringContent(json,System.Text.Encoding.UTF8,"application/json");
            var response = await hc.DeleteAsync("/games/"+b._id+"?rev="+b._rev);
            if(response.IsSuccessStatusCode)
            {
                Console.WriteLine("Game deleted");
                return 1;
            }
            else Console.WriteLine("Game not deleted");
            return 1;
        }
        }
    }

