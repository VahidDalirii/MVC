using MongoDB.Bson;
using System.Diagnostics.CodeAnalysis;

namespace CentiroHomeAssignment.Models
{
    public class OrderRow
    {
        [NotNull] 
        public ObjectId Id { get; set; }
        [NotNull]
        public string OrderNumber { get; set; }
        [NotNull]
        public string OrderLineNumber { get; set; }
        [NotNull]
        public string ProductNumber { get; set; }
        [NotNull]
        public string Quantity { get; set; }
        [NotNull]
        public string Name { get; set; }
        public string Description { get; set; }
        [NotNull]
        public string Price { get; set; }
        [NotNull]
        public string ProductGroup { get; set; }
        [NotNull]
        public string OrderDate { get; set; }
        [NotNull]
        public string CustomerName { get; set; }
        [NotNull]
        public string CustomerNumber { get; set; }
    }
}
