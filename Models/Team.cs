using System;
using System.Collections.Generic;

namespace UserService.Models
{
    public class Team
    {
        public string _id {get; set;}
        public string _rev {get; set;}
        public string name {get;set;}
        public bool isPlaying {get;set;}
        public int stage {get;set;}
        public List<Player> players {get;set;}
        public List<Game> games {get;set;}
        public Team(){}
    }
    public class creatingTeam
    {
        public string _id {get; set;}
        public string name {get;set;}
        public bool isPlaying {get;set;}
        public int stage {get;set;}
        public List<Player> players {get;set;}
        public List<Game> games {get;set;}
        public creatingTeam(){}
    }
}
