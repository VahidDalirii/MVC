using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Models
{
    public class Order
    {
        public ObjectId Id { get; set; }
        public string Title { get; set; }
        public string Cost { get; set; }
        public ObjectId CustomerId { get; set; }

    }
}
