using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentiroHomeAssignment.Models
{
    public class FileModel
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string FileName { get; set; }
    }
}
