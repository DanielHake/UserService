using System;

namespace UserService.Models
{
    public class Game
    {
        public string _id {get; set;}
        public string _rev {get; set;}
        public Team teamA {get;set;}
        public Team teamB {get;set;}
        public string date {get;set;}
        public int goalsA {get;set;}
        public int goalsB {get;set;}
        public int stage {get;set;}
        public Game(){}
    }
    public class creatingGame
    {
        public string _id {get; set;}
        public Team teamA {get;set;}
        public Team teamB {get;set;}
        public string date {get;set;}
        public int goalsA {get;set;}
        public int goalsB {get;set;}
        public int stage {get;set;}
        public creatingGame(){}
    }
}
