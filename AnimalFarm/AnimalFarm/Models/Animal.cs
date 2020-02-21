using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnimalFarm.Models
{
    public class Animal
    {
        public ObjectId Id { get; set; }
        public string Species { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }

        public ObjectId? OwnerId { get; set; } = null;
    }
}
