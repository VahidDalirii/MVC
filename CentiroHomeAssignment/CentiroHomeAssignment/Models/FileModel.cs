using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace CentiroHomeAssignment.Models
{
    public class FileModel
    {
        [BsonId]
        [NotNull]
        public ObjectId Id { get; set; }
        [NotNull]
        public string FileName { get; set; }
    }
}
