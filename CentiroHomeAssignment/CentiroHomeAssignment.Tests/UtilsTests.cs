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
            var utils = new Utils();
            var mockedUtils = new Mock<IUtils>();
            mockedUtils.Setup(e => e.OrderIsAlreadyRegistered(It.IsAny<OrderRow>())).Returns(false);
            //mockedUtils.Setup(x => x.AddOrderToDatabase(It.IsAny<OrderRow>())).

            var orders = utils.AddOrdersFromFiles(new List<OrderRow>(), "TestFiles\\");

            Assert.AreEqual(orders.Count, 13);

        }
    }
}
