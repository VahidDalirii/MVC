using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerListTests
{
    [TestClass]
    public class CustomerListWebAppTests
    {
        [TestMethod]
        [DataRow("Hello",false), DataRow("456", true), DataRow("hi32bye", false),DataRow("87245",true)]
        public void IsCostOnlyDigitsTest(string cost, bool expected)
        {

            bool result = OrderRepository.IsCostOnlyDigits(cost);

            Assert.AreEqual(expected, result);
        }
    }
}
