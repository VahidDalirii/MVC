using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingSystemApp.Models
{
    public class Account
    {
        public ObjectId Id { get; set; }
        public ObjectId CustomerId { get; set; }
        public List<ObjectId> TransactionIds { get; set; } = null;
        public string Type { get; set; }
        public DateTime OpenDate { get; set; }
        public DateTime CloseDate { get; set; }
        public int Balance { get; set; }

        //public Account(string type, DateTime openDate, DateTime closeDate, int balance)
        //{
        //    Type = type;
        //    OpenDate = openDate;
        //    CloseDate = closeDate;
        //    Balance = balance;
        //}

    }
}
