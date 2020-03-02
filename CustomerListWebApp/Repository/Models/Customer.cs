using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Models
{
    public class Customer
    {
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public string TelNumber { get; set; }
        public string Notes { get; set; }
    }
}
