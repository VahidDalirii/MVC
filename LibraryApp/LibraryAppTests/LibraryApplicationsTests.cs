using LibraryApp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryAppTests
{
    [TestClass]
    public class LibrarayAppTests
    {
        LibraryApplication app = new LibraryApplication();

        [TestMethod]
        [DataRow("1234",true),DataRow("12ab34",false),DataRow("2020",true),DataRow("1o0o0o0o",false)]
        public void IsDigitsOnlyTest(string str, bool expected)
        {
            bool result = app.IsDigitsOnly(str);
            Assert.AreEqual(result, expected);
        }

        [TestMethod]
        [DataRow("2020,01,01",true), DataRow("2010.3,6",true),DataRow("20200,3,8",false)]
        [DataRow("2000,01,1", true), DataRow("2010.30,6", false), DataRow("2020,3,80", false)]
        public void IsDateInRightFormatTest(string date,bool expected)
        {
            bool result = app.IsDateInRightFormat(date);
            Assert.AreEqual(result, expected);
        }


    }


}
