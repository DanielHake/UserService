using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UserService.Models;
using System.Net.Http;
using Newtonsoft.Json;


namespace UserService.Controllers
{
    [Route("api/[controller]")]
    public class loginController : Controller
    {

        
        static List<User> Users = new List<User>();

             

        // get User by Id - returns User or null 
        [HttpGet]
        [Route("getUser/{userId}")]
        public async Task<User> getUser(string userId) 
        {
            string text;
            User u = null;
            // get from db
            var hc = Helpers.CouchDBConnect.GetClient("users");
            HttpResponseMessage response = await hc.GetAsync("/users/"+userId);
            if(!response.IsSuccessStatusCode)
            {
                Console.WriteLine("failed to get user : " + userId);
                return null;
            }
            HttpContent content = response.Content;
            text = await content.ReadAsStringAsync();
            Console.WriteLine(text);
            if (!text.Contains("_id"))
                Console.WriteLine("user not found in db");
            else
            {
                u = (User) JsonConvert.DeserializeObject(text,typeof(User));
            }
        
            return u;
        }

        // create new user and insert to db if not exists yet 
        [HttpPost]
        [Route("createUser")]
        public async Task<dynamic> CreateUser([FromBody]creatingUser u)
        {
            var hc = Helpers.CouchDBConnect.GetClient("users");
            // create http body
            string json = JsonConvert.SerializeObject(u);
            HttpContent htc = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            // check the response
            HttpResponseMessage response = await hc.PostAsync("", htc);
            if(response.IsSuccessStatusCode)
            {
                Console.WriteLine("user added"); 
                return 1;
            }
            return -1;
        }

        // update specific user in db
        // api/login
        [HttpPost]
        [Route("updateUser")]
        public async Task<int> UpdateUser([FromBody]User u)
        {
            var hc = Helpers.CouchDBConnect.GetClient("users");
            string json = JsonConvert.SerializeObject(u);
            Console.WriteLine(json);
            HttpContent htc = new StringContent(json,System.Text.Encoding.UTF8,"application/json");
            using (HttpResponseMessage response = await hc.PutAsync("/users/"+u._id,htc))
            {
                if(response.IsSuccessStatusCode)
                {
                    Console.WriteLine("User updated");
                    return 1;
                }
                else 
                    Console.WriteLine("User update failed");
            }
            return -1;
        }

        // delete user from table
        [HttpPost]
        [Route("deleteUser")]
        public async Task<int> Delete([FromBody]User u)
        {
            var hc = Helpers.CouchDBConnect.GetClient("users");
            using(HttpResponseMessage response = await hc.DeleteAsync("/users/"+ u._id+"?rev="+u._rev))
            {
                if(response.IsSuccessStatusCode)
                {
                    Console.WriteLine("User deleted");
                    return 1;
                }
                else 
                    Console.WriteLine("User delete failed");
            }
            return -1;
        }
    }
}
