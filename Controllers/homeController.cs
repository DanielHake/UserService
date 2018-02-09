using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UserService.Models;
using System.Net.Http;
using Newtonsoft.Json;

namespace UserService.Controllers {
    
    public class homeController : Controller 
    {
        public ActionResult index()
        {
            return View();
        }
    }
}