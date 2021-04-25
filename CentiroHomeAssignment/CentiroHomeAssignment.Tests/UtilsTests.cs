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
        public void AddOrdersFromFiles_ReadFilesInDirectory_ReturnAllOrdersInFiles()
        {
            var path = "TestFiles\\";
            var utils = new Utils();
            var mockedUtils = new Mock<IUtils>();
            
            
            mockedUtils.Setup(e => e.FileIsAlreadyRegistered(It.IsAny<string>())).Returns(false);
            mockedUtils.Setup(e => e.OrderIsAlreadyRegistered(It.IsAny<OrderRow>())).Returns(false);
            mockedUtils.Setup(e => e.AddedOrderSuccessfullyToDatabase(It.IsAny<OrderRow>())).Returns(true);
            mockedUtils.Setup(e => e.AddOrdersFromFiles(It.IsAny<List<OrderRow>>(), path)).Returns(utils.AddOrdersFromFiles(new List<OrderRow>(), path));

            OrderRepository or = new OrderRepository(mockedUtils.Object);
            var orders = or.AddOrdersFromFiles(new List<OrderRow>(), path);

            mockedUtils.VerifyAll();
            Assert.AreEqual(orders.Count, 13);
            
        }
    }
}
