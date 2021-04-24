using CentiroHomeAssignment.Models;
using CentiroHomeAssignment.Repositories;
using CentiroHomeAssignment.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace CentiroHomeAssignment.Tests
{
    [TestClass]
    public class UtilsTests
    {
        [TestMethod]
        [DeploymentItem("TestFiles\\")]
        public void AddOrdersFromFiles_ReadFilesInDirectory_ReturnAllOrdersInFiles()
        {
            //var utils = new Mock<Utils>();
            //utils.Setup(e => e.)
            var orderRepository = new Mock<IOrderRepository>();
            orderRepository.Setup(e => e.CreateOrder(null));

            var orders = Utils.AddOrdersFromFiles(new List<OrderRow>(), "TestFiles\\");

            Assert.AreEqual(orders.Count, 13);

        }
    }
}
