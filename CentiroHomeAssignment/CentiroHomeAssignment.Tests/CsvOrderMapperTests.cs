using CentiroHomeAssignment.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace CentiroHomeAssignment.Tests
{
    [TestClass]
    public class CsvOrderMapperTests
    {
        [TestMethod]
        public void MapOrder_MapListToOrder_ShouldMapSuccessfully()
        {
            List<string> orderElements = new List<string>
            {
                "17835","0001","123-100","2","1x1 Red Piece","","0.05","Normal","2021-04-24","Kalle Svensson","265849"
            };

            var order = CsvOrderMapper.MapRow(orderElements);

            Assert.AreEqual(order.OrderNumber, "17835");
            Assert.AreEqual(order.ProductNumber, "123-100");
            Assert.AreEqual(order.Quantity, 2);
            Assert.AreEqual(order.Description, "");
            Assert.AreEqual(order.CustomerName, "Kalle Svensson");
            Assert.AreEqual(order.CustomerNumber, "265849");
        }
    }
}
