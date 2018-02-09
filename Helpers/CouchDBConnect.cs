using System;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using UserService.Models;

namespace UserService.Helpers
{
    public static class CouchDBConnect
    {
        private static string host = "https://aa8d350d-da37-4221-9c93-a39d8ce02db8-bluemix:ae89db5254e240a46cc64094ebdc85cb84432bfc82c831c0f60f692a5b53f472@aa8d350d-da37-4221-9c93-a39d8ce02db8-bluemix.cloudant.com/{0}";
        public static HttpClient GetClient(string db) 
        {
            var hc = new HttpClient();
            hc.BaseAddress = new Uri(string.Format(host,db));
            hc.DefaultRequestHeaders.Clear();
            hc.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));         
            Console.WriteLine(hc.DefaultRequestHeaders);
            
            return hc;
        }
    }
}