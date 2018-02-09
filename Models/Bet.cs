using System;

namespace UserService.Models
{
    public class Bet
    {
        public string _id {get; set;}
        public string _rev {get; set;}
        public User user {get;set;}
        public Game game {get;set;}
        public int scoreTeamA {get;set;}
        public int scoreTeamB {get;set;}
        public string note {get;set;}
        public bool isDouble {get;set;}
        public Bet(){}
    }
    public class creatingBet 
    {
        public string _id {get; set;}
        public User user {get;set;}
        public Game game {get;set;}
        public int scoreTeamA {get;set;}
        public int scoreTeamB {get;set;}
        public bool isDouble {get;set;}
        public string note {get;set;}
        public creatingBet(){}
    }
}
