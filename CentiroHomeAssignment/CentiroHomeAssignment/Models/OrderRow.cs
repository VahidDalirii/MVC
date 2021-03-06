﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace CentiroHomeAssignment.Models
{
    public class OrderRow
    {
        [BsonId]
        [NotNull]
        public ObjectId Id { get; set; }
        [NotNull]
        public string OrderNumber { get; set; }
        [NotNull]
        public string OrderLineNumber { get; set; }
        [NotNull]
        public string ProductNumber { get; set; }
        [NotNull]
        public int Quantity { get; set; }
        [NotNull]
        public string Name { get; set; }
        [AllowNull]
        public string Description { get; set; }
        [NotNull]
        public double Price { get; set; }
        [NotNull]
        public string ProductGroup { get; set; }
        [NotNull]
        [DataType(DataType.Date)]
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime OrderDate { get; set; }
        [NotNull]
        public string CustomerName { get; set; }
        [NotNull]
        public string CustomerNumber { get; set; }
    }
}
