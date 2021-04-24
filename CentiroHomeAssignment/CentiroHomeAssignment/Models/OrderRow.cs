using MongoDB.Bson;

namespace CentiroHomeAssignment.Models
{
    public class OrderRow
    {
        public ObjectId Id { get; set; }
        public string OrderNumber { get; set; }
        public string OrderLineNumber { get; set; }
        public string ProductNumber { get; set; }
        public string Quantity { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Price { get; set; }
        public string ProductGroup { get; set; }
        public string OrderDate { get; set; }
        public string CustomerName { get; set; }
        public string CustomerNumber { get; set; }
    }
}
