using CustomerListApp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerListTests
{
    [TestClass]
    public class CustomerListAppTests
    {
        [TestMethod]
        [DataRow(new string[] {"25","25","50" }, 100), DataRow(new string[] { "100", "200", "300","400" }, 1000), DataRow(new string[] { "25", "225", "500", "600", "150"}, 1500)]
        public void GetSumOfOrdersCostTest(string[] costs, int expected)
        {
            CustomerListApplication app = new CustomerListApplication();

            int result = app.GetSumOfOrdersCost(costs);

            Assert.AreEqual(expected, result);
        }
    }
}
