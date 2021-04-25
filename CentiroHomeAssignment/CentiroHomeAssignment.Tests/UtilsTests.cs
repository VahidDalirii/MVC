using CentiroHomeAssignment.Models;
using CentiroHomeAssignment.Repositories;
using CentiroHomeAssignment.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CentiroHomeAssignment.Tests
{
    [TestClass]
    public class UtilsTests
    {
        [TestMethod]
        public void AddOrdersFromFiles_MockUtilsClassAndMethods_ReturnAllOrdersInFiles()
        {
            var path = "TestFiles\\";
            var utils = new Utils();
            var mockedUtils = new Mock<CreateAddOrdersFromFilesMock>();

            mockedUtils.Setup(e => e.FileIsAlreadyRegistered(It.IsAny<string>())).Returns(false);
            mockedUtils.Setup(e => e.OrderIsAlreadyRegistered(It.IsAny<OrderRow>())).Returns(false);
            mockedUtils.Setup(e => e.AddedOrderSuccessfullyToDatabase(It.IsAny<OrderRow>())).Returns(true);

            MockOrderRepository or = new MockOrderRepository(mockedUtils.Object);
            var orders = or.AddOrdersFromFiles(new List<OrderRow>(), path);

            mockedUtils.VerifyAll();
            Assert.AreEqual(orders.Count, 13);
            Assert.AreEqual(orders[5].OrderNumber, "17890");
            Assert.AreEqual(orders[8].Name, "Little Guys");
            Assert.AreEqual(orders[10].OrderLineNumber, "0003");

        }

        [TestMethod]
        public void GetTrimedOrderValues_SenUntrimedOrder_ShouldTrimAllPropertyValues()
        {
            var order = new OrderRow
            {
                OrderNumber = "  17835  ",
                OrderLineNumber = "     0002    ",
                ProductNumber = "	4000-AAA",
                Quantity = "  2  ",
                Name = "  Test",
                Description = "   Some des  ",
                Price = " price",
                ProductGroup = "  Normal ",
                OrderDate = "2021-04-24",
                CustomerName = "   Centiro  ",
                CustomerNumber = "   1234"
            };

            var utils = new Utils();
            order = utils.GetTrimedOrderValues(order);

            Assert.AreEqual(order.OrderNumber, "17835");
            Assert.AreEqual(order.ProductNumber, "4000-AAA");
            Assert.AreEqual(order.ProductGroup, "Normal");
            Assert.AreEqual(order.CustomerName, "Centiro");
        }

        public class MockOrderRepository
        {
            public CreateAddOrdersFromFilesMock _mock;
            public MockOrderRepository(CreateAddOrdersFromFilesMock mock)
            {
                _mock = mock;
            }

            public List<OrderRow> AddOrdersFromFiles(List<OrderRow> orders, string path)
            {
                return _mock.AddOrdersFromFiles(orders, path);
            }
        }
        public class CreateAddOrdersFromFilesMock
        {
            public virtual bool FileIsAlreadyRegistered(string fileName)
            {
                return false;
            }

            public virtual bool OrderIsAlreadyRegistered(OrderRow order)
            {
                return false;
            }

            public virtual bool AddedOrderSuccessfullyToDatabase(OrderRow order)
            {
                return true;
            }

            public List<OrderRow> AddOrdersFromFiles(List<OrderRow> orders, string path)
            {
                try
                {
                    if (!Directory.Exists(path))
                        throw new Exception("Path does not exists");

                    var files = FileService.GetFiles(path);

                    foreach (var file in files)
                    {
                        if (Path.GetExtension(file).Equals(".txt", StringComparison.InvariantCultureIgnoreCase))
                        {
                            var fileName = Path.GetFileName(file);
                            if (FileIsAlreadyRegistered(fileName))
                                continue;
                            var parser = new FileParser<OrderRow>('|', true, false, "txt");
                            var rows = parser.GetRows(file, Encoding.UTF8);
                            foreach (var row in rows)
                            {
                                var splitedRow = parser.GetSplitedRow(row);
                                var order = new OrderRow();
                                try
                                {
                                    order = CsvOrderMapper.MapRow(splitedRow);
                                }
                                catch (NullReferenceException)
                                {
                                    continue;
                                }

                                if (!OrderIsAlreadyRegistered(order))
                                {
                                    if (AddedOrderSuccessfullyToDatabase(order))
                                        orders.Add(order);
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception($" Erro: {ex.Message}");
                }

                return orders;
            }

        }


    }
}
