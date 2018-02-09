using System.Collections.Generic;
using System;


namespace UserService.Models
{
    public class Table
    {
        public string _id {get; set;}
        public string _rev {get; set;}
        public string name {get;set;}
        public string adminId {get;set;}
        public string password {get;set;}
        public List<User> users {get;set;}
        public List<Game> games {get;set;}
        public Table(){}

    }
    public class creatingTable
    {
        public string _id {get; set;}
        public string name {get;set;}
        public string adminId {get;set;}
        public string password {get;set;}
        public List<User> users {get;set;}
        public List<Game> games {get;set;}
        public creatingTable(){}

    }
}
