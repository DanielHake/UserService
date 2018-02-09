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
    public class tableController : Controller 
    {
        // check if table exists
        [HttpGet]
        [Route("getTable/{tableId}")]
        public async Task<Table> getTable(string tableId) 
        {
            Table table = null;
            string json;
            var hc = Helpers.CouchDBConnect.GetClient("tables");
            var response = await hc.GetAsync("/tables/"+tableId);
            if(response.IsSuccessStatusCode)
            {
                HttpContent content = response.Content;
                json = await content.ReadAsStringAsync();
                if (json.Contains("_id"))
                {
                    table = (Table) JsonConvert.DeserializeObject<Table>(json);
                    return table;
                }
                else Console.WriteLine("table not found in db "+ tableId);
            }
            Console.WriteLine("http error : " + response.StatusCode + " - table not found in db "+ tableId);
            return null;
        }

        // add table to db
        [HttpPost]
        [Route("createTable")]
        public async Task<int> postTable([FromBody]creatingTable t)
        {
            var hc = Helpers.CouchDBConnect.GetClient("tables");
            string json = JsonConvert.SerializeObject(t);
            HttpContent htc = new StringContent(json,System.Text.Encoding.UTF8,"application/json");
            var response = await hc.PostAsync("",htc);
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("table created");
                return 1;
            }
            else 
                Console.WriteLine("table not created db ");
            return 1;
        }

        // update exsisting table
        [HttpPost]
        [Route("updateTable")]
        public async Task<int> updateTable([FromBody]Table t) 
        {
            var hc = Helpers.CouchDBConnect.GetClient("tables");
            string json = JsonConvert.SerializeObject(t);
            HttpContent htc = new StringContent(json,System.Text.Encoding.UTF8,"application/json");
            var response = await hc.PutAsync("/tables/"+t._id,htc);
            if(response.IsSuccessStatusCode)
            {
                Console.WriteLine("table updated");
                return 1;
            }
            else Console.WriteLine("table not updated");
            return -1;
        }

        // delete tables
        [HttpPost]
        [Route("deleteTable")]
        public async Task<int> deleteTable([FromBody]Table t)
        {
            var hc = Helpers.CouchDBConnect.GetClient("tables");
            string json = JsonConvert.SerializeObject(t);
            HttpContent htc = new StringContent(json,System.Text.Encoding.UTF8,"application/json");
            var response = await hc.DeleteAsync("/tables/"+t._id+"?rev="+t._rev);
            if(response.IsSuccessStatusCode)
            {
                Console.WriteLine("table deleted");
                return 1;
            }
            else Console.WriteLine("table not deleted");
            return 1;
        }
    }
}