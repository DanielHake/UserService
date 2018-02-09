using System;

namespace UserService.Models
{
    public class Player
    {
        public string _id {get; set;}
        public string _rev {get; set;}
        public string name {get;set;}
        public Team team {get;set;}
        public int goals {get;set;}

        public Player(){}
    }
    public class creatingPlayer
    {
        public string _id {get; set;}
        public string name {get;set;}
        public Team team {get;set;}
        public int goals {get;set;}

        public creatingPlayer(){}
    }
}
