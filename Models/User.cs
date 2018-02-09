using System;
using System.Collections.Generic;


namespace UserService.Models
{
    public class User
    {
        public string _id {get; set;}
        public string _rev {get; set;}
        public string name {get;set;}
        public string password {get; set;}
        public int totalScore {get;set;}
        public List<Bet> bets {get;set;} 
        public List<Table> tables {get;set;}
        public User()
        {
            this._id = "";
            this.name = "";
            this.password = "";
            this.totalScore = 0;
            this.bets = new List<Bet>(34);
            this.tables = new List<Table>();
        }
        public User(string name, string password)
        {
            this._id = name + password;
            this.name = name;
            this.password = password;
            this.totalScore = 0;
            this.bets = new List<Bet>(34);
            this.tables = new List<Table>();
        }
    }
    public class creatingUser
    {
        public string _id {get; set;}
        public string name {get;set;}
        public string password {get; set;}
        public int totalScore {get;set;}
        public List<Bet> bets {get;set;} 
        public List<Table> tables {get;set;}
        public creatingUser(){}
    }
    // public class Token {
    //     public string _id {get; set;}
    //     public int ttl {get ;set;}
        
    //     public DateTime create {get; set;}

    //     public Token(){}
    // }
}