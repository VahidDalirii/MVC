﻿using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingSystemApp.Models
{
    public class Transaction
    {
        public ObjectId Id { get; set; }
        public ObjectId AccountId { get; set; }
        public string Type { get; set; }
        public int Amount { get; set; }
        public DateTime Date { get; set; }
        public string Message { get; set; }

        //public Transaction(string type, int amount, DateTime date, string message)
        //{
        //    Type = type;
        //    Amount = amount;
        //    Date = date;
        //    Message = message;
        //}

    }
}
