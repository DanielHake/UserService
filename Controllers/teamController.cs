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
    public class teamController : Controller 
    {
        // check if Team exists
        [HttpGet]
        [Route("getTeam/{teamId}")]
        public async Task<Team> getTeam(string teamId) 
        {
            Team team = null;
            string json;
            var hc = Helpers.CouchDBConnect.GetClient("teams");
            var response = await hc.GetAsync("/teams/"+teamId);
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine("team not found in db " + teamId);
                return null;
            }
            else 
            {
                HttpContent content = response.Content;
                json = await content.ReadAsStringAsync();
                if(json.Contains("_id"))
                {
                    Console.WriteLine("team found");
                    team = (Team) JsonConvert.DeserializeObject<Team>(json);
                    return team;
                }
                else Console.WriteLine("team found in db " + teamId);
            }
            return null;
        }

        // add team to db
        [HttpPost]
        [Route("createTeam")]
        public async Task<int> postTeam([FromBody]creatingTeam t)
        {
            var hc = Helpers.CouchDBConnect.GetClient("teams");
            string json = JsonConvert.SerializeObject(t);
            Console.WriteLine(json);
            HttpContent htc = new StringContent(json,System.Text.Encoding.UTF8,"application/json");
            var response = await hc.PostAsync("",htc);
            if(response.IsSuccessStatusCode)
            {
                Console.WriteLine("team created");
                return 1;
            }
            return -1;
        }

        // update exsisting team
        [HttpPost]
        [Route("updateTeam")]
        public async Task<int> updateTeam([FromBody]Team t) 
        {
            var hc = Helpers.CouchDBConnect.GetClient("teams");
            string json = JsonConvert.SerializeObject(t);
            HttpContent htc = new StringContent(json,System.Text.Encoding.UTF8,"application/json");
            var response = await hc.PutAsync("/teams/"+t._id,htc);
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("team updated");                
                return 1;
            }
            else Console.WriteLine("team not updated");
            return 1;
        }

        // delete team
        [HttpPost]
        [Route("deleteTeam")]
        public async Task<int> deleteTeam([FromBody]Team t)
        {
            var hc = Helpers.CouchDBConnect.GetClient("teams");
            var response = await hc.DeleteAsync("/teams/"+t._id+"?rev="+t._rev);
            if(response.IsSuccessStatusCode)
            {
                Console.WriteLine("Team deleted");
                return 1;
            }
            else Console.WriteLine("Team not deleted");
            return -1;
        }
    }
}