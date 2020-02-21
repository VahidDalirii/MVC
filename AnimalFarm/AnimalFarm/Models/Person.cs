using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnimalFarm.Models
{
    public class Person
    {
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public string SSN { get; set; }
        public List<ObjectId> AnimalIds { get; set; }

    }
}
